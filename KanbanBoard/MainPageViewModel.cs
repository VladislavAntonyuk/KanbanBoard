using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MvvmHelpers;
using Xamarin.Forms;

namespace KanbanBoard
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Column> columns;

        public MainPageViewModel()
        {
            UpdateCollection();
        }

        public ICommand AddColumn => new Command(() =>
        {
            var column = new Column() { Name = "New column", Order=columns.Count+1 };
            using (ApplicationContext db = new ApplicationContext(App.dbPath))
            {
                db.Columns.Add(column);
                db.SaveChanges();
            }
            UpdateCollection();
        });

        public ICommand AddCard => new Command<int>((columnId) =>
        {
            using (ApplicationContext db = new ApplicationContext(App.dbPath))
            {
                var column = db.Columns.Include(c=>c.Cards).First(c => c.Id == columnId);
                if (column.WIP > column.Cards.Count)
                {
                    db.Cards.Add(new Card() { Name = "New card", ColumnId = columnId, Order = column.Cards.Count + 1 });
                    db.SaveChanges();
                    UpdateCollection();
                }
            }
        });

        public ICommand DeleteCard => new Command<Card>((card) =>
        {
            using (ApplicationContext db = new ApplicationContext(App.dbPath))
            {
                db.Cards.Remove(card);
                db.SaveChanges();
            }
            UpdateCollection();
        });

        public ObservableCollection<Column> Columns
        {
            get => columns;
            set
            {
                columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }

        private void UpdateCollection()
        {
            using (ApplicationContext db = new ApplicationContext(App.dbPath))
            {
                Columns = db.Columns.Include(c=>c.Cards).OrderBy(c=>c.Order).ToList().Select(c => OrderCards(c)).ToObservableCollection();
            }
        }

        private Column OrderCards(Column c)
        {
            c.Cards = c.Cards.OrderBy(card => card.Order).ToList();
            return c;
        }
    }
}
