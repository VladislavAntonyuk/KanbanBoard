using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KanbanBoard.Db;
using KanbanBoard.Models;
using Microsoft.EntityFrameworkCore;
using MvvmHelpers;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace KanbanBoard
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<ColumnInfo> _columns;
        private Card _dragCard;
        private int _position;

        public MainPageViewModel()
        {
            RefreshCommand.Execute(null);
        }

        public ICommand RefreshCommand => new Command(UpdateCollection);

        public ICommand DropCommand => new Command<ColumnInfo>(async columnInfo =>
        {
            if (_dragCard is null || columnInfo.Column.Cards.Count >= columnInfo.Column.Wip) return;
            await using (var db = new ApplicationContext(App.DbPath))
            {
                var cardToUpdate = await db.Cards.FirstOrDefaultAsync(x => x.Id == _dragCard.Id);
                if (cardToUpdate is not null)
                {
                    cardToUpdate.ColumnId = columnInfo.Column.Id;
                    db.Cards.Update(cardToUpdate);
                    await db.SaveChangesAsync();
                }
            }

            UpdateCollection();
            Position = columnInfo.Index;
        });

        public ICommand DragStartingCommand => new Command<Card>(card => { _dragCard = card; });

        public ICommand DropCompletedCommand => new Command(() => { _dragCard = null; });

        public ICommand AddColumn => new Command(async () =>
        {
            var columnName = await UserPromptAsync("New column", "Enter column name", Keyboard.Default);
            if (string.IsNullOrWhiteSpace(columnName)) return;

            int wip;
            do
            {
                var wipString = await UserPromptAsync("New column", "Enter column WIP", Keyboard.Numeric);
                if (string.IsNullOrWhiteSpace(wipString)) return;

                int.TryParse(wipString, out wip);
            } while (wip < 0);

            var column = new Column {Name = columnName, Wip = wip, Order = _columns.Count + 1};
            await using (var db = new ApplicationContext(App.DbPath))
            {
                await db.Columns.AddAsync(column);
                await db.SaveChangesAsync();
            }

            UpdateCollection();
            await ToastAsync("Column is added");
        });

        public ICommand AddCard => new Command<int>(async columnId =>
        {
            await using (var db = new ApplicationContext(App.DbPath))
            {
                var column = await db.Columns.Include(c => c.Cards).FirstAsync(c => c.Id == columnId);
                var columnInfo = new ColumnInfo(0, column);
                if (columnInfo.IsWipReached)
                {
                    await WipReachedToastAsync("WIP is reached");
                    return;
                }

                var cardName = await UserPromptAsync("New card", "Enter card name", Keyboard.Default);
                if (string.IsNullOrWhiteSpace(cardName)) return;

                var cardDescription = await UserPromptAsync("New card", "Enter card description", Keyboard.Default);
                await db.Cards.AddAsync(new Card
                {
                    Name = cardName, Description = cardDescription, ColumnId = columnId,
                    Order = column.Cards.Count + 1
                });
                await db.SaveChangesAsync();
            }

            UpdateCollection();
            await ToastAsync("Card is added");
        });

        public ICommand DeleteCard => new Command<Card>(async card =>
        {
            var result = await AlertAsync("Delete card", $"Do you want to delete card \"{card.Name}\"?");
            if (!result) return;

            var shouldCancel = await SnackbarAsync("The card is about to be removed", "Cancel",
                () => ToastAsync("Task is cancelled"));

            if (!shouldCancel)
            {
                await using var db = new ApplicationContext(App.DbPath);
                db.Cards.Remove(card);
                await db.SaveChangesAsync();
                UpdateCollection();
            }
        });

        public ICommand DeleteColumn => new Command<ColumnInfo>(async columnInfo =>
        {
            var result = await AlertAsync("Delete column",
                $"Do you want to delete column \"{columnInfo.Column.Name}\" and all its cards?");
            if (!result) return;

            Columns.Remove(columnInfo);
            var shouldCancel = await SnackbarAsync("The column is removed", "Cancel", () =>
            {
                Columns.Add(columnInfo);
                return Task.CompletedTask;
            });

            if (!shouldCancel)
            {
                await using var db = new ApplicationContext(App.DbPath);
                db.Columns.Remove(columnInfo.Column);
                await db.SaveChangesAsync();
            }

            UpdateCollection();
        });

        public ObservableCollection<ColumnInfo> Columns
        {
            get => _columns;
            set => SetProperty(ref _columns, value);
        }

        public int Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private void UpdateCollection()
        {
            IsBusy = true;
            using (var db = new ApplicationContext(App.DbPath))
            {
                Columns = db.Columns.Include(c => c.Cards)
                    .OrderBy(c => c.Order)
                    .ToList()
                    .Select(OrderCards)
                    .ToObservableCollection();
                Position = 0;
            }

            IsBusy = false;
        }

        private static ColumnInfo OrderCards(Column c, int columnNumber)
        {
            c.Cards = c.Cards.OrderBy(card => card.Order).ToList();
            return new ColumnInfo(columnNumber, c);
        }

        private static Task<bool> AlertAsync(string title, string message)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, "Yes", "No");
        }

        private static Task<string> UserPromptAsync(string title, string message, Keyboard keyboard)
        {
            return Application.Current.MainPage.DisplayPromptAsync(title, message, keyboard: keyboard);
        }

        private static Task<bool> SnackbarAsync(string title, string buttonText, Func<Task> task)
        {
            return Application.Current.MainPage.DisplaySnackBarAsync(title, buttonText, task, TimeSpan.FromSeconds(3));
        }

        private static Task ToastAsync(string title)
        {
            return Application.Current.MainPage.DisplayToastAsync(title, 3500);
        }

        private static Task WipReachedToastAsync(string title)
        {
            return Application.Current.MainPage.DisplayToastAsync(
                new ToastOptions
                {
                    BackgroundColor = Color.Navy,
                    Duration = TimeSpan.FromSeconds(5),
                    MessageOptions = new MessageOptions
                    {
                        Message = title,
                        Padding = new Thickness(140),
                        Foreground = Color.Teal,
                        Font = Font.SystemFontOfSize(25)
                    }
                });
        }
    }
}