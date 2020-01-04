namespace KanbanBoard
{
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows.Input;
	using Db;
	using Microsoft.EntityFrameworkCore;
	using Models;
	using MvvmHelpers;
	using Xamarin.Forms;

	public class MainPageViewModel : BaseViewModel
	{
		private ObservableCollection<ColumnInfo> _columns;

		public MainPageViewModel()
		{
			UpdateCollection();
		}

		public ICommand AddColumn => new Command(async () =>
		{
			var columnName = await UserPromptTask("New column", "Enter column name", Keyboard.Default);
			if (string.IsNullOrWhiteSpace(columnName))
			{
				return;
			}

			int wip;
			do
			{
				var wipString = await UserPromptTask("New column", "Enter column WIP", Keyboard.Numeric);
				if (string.IsNullOrWhiteSpace(wipString))
				{
					return;
				}

				int.TryParse(wipString, out wip);
			} while (wip < 0);

			var column = new Column {Name = columnName, Wip = wip, Order = _columns.Count + 1};
			using (var db = new ApplicationContext(App.DbPath))
			{
				db.Columns.Add(column);
				db.SaveChanges();
			}

			UpdateCollection();
		});

		public ICommand AddCard => new Command<int>(async columnId =>
		{
			using (var db = new ApplicationContext(App.DbPath))
			{
				var column = db.Columns.Include(c => c.Cards).First(c => c.Id == columnId);
				var columnInfo = new ColumnInfo(column);
				if (columnInfo.IsWipReached)
				{
					return;
				}

				var cardName = await UserPromptTask("New card", "Enter card name", Keyboard.Default);
				if (string.IsNullOrWhiteSpace(cardName))
				{
					return;
				}

				var cardDescription = await UserPromptTask("New card", "Enter card description", Keyboard.Default);
				db.Cards.Add(new Card
				{
					Name = cardName, Description = cardDescription, ColumnId = columnId,
					Order = column.Cards.Count + 1
				});
				db.SaveChanges();
				UpdateCollection();
			}
		});

		public ICommand DeleteCard => new Command<Card>(async card =>
		{
			var result = await Application.Current.MainPage.DisplayAlert("Delete card",
				$"Dou you want to delete card \"{card.Name}\"?", "Yes", "No");
			if (!result)
			{
				return;
			}

			using (var db = new ApplicationContext(App.DbPath))
			{
				db.Cards.Remove(card);
				db.SaveChanges();
			}

			UpdateCollection();
		});

		public ICommand DeleteColumn => new Command<ColumnInfo>(async columnInfo =>
		{
			var result = await Application.Current.MainPage.DisplayAlert("Delete column",
				$"Dou you want to delete column \"{columnInfo.Column.Name}\" and all its cards?", "Yes", "No");
			if (!result)
			{
				return;
			}

			using (var db = new ApplicationContext(App.DbPath))
			{
				db.Columns.Remove(columnInfo.Column);
				db.SaveChanges();
			}

			UpdateCollection();
		});

		public ObservableCollection<ColumnInfo> Columns
		{
			get => _columns;
			set
			{
				_columns = value;
				OnPropertyChanged(nameof(Columns));
			}
		}

		private void UpdateCollection()
		{
			// This method can be used to sync local changes with remote database
			using (var db = new ApplicationContext(App.DbPath))
			{
				Columns = db.Columns.Include(c => c.Cards)
					.OrderBy(c => c.Order)
					.ToList()
					.Select(OrderCards)
					.ToObservableCollection();
			}
		}

		private static ColumnInfo OrderCards(Column c)
		{
			c.Cards = c.Cards.OrderBy(card => card.Order).ToList();
			return new ColumnInfo(c);
		}

		private static Task<string> UserPromptTask(string title, string message, Keyboard keyboard)
		{
			return Application.Current.MainPage.DisplayPromptAsync(title, message, keyboard: keyboard, initialValue:"");
		}
	}
}