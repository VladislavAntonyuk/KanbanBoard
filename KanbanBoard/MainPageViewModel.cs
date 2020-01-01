using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace KanbanBoard
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Column> columns;
        private ObservableCollection<Card> cards = new ObservableCollection<Card>();

        public MainPageViewModel()
        {
            cards.Add(new Card() { Name = "Card 1", Description = "Description for card 1", Order = 0 , ColumnId = 0});
            cards.Add(new Card() { Name = "Card 2", Description = "Description for card 2", Order = 1 , ColumnId = 0});
            cards.Add(new Card() { Name = "Card 1", Description = "Description for card 1", Order = 0 , ColumnId = 1});
            cards.Add(new Card() { Name = "Card 2", Description = "Description for card 2", Order = 1 , ColumnId=1});            
            var columnsCollection = new ObservableCollection<Column>();
            columnsCollection.Add(new Column() { Id=0,Name = "ToDo", Order = 0, WIP = 0, Cards = cards.Where(c => c.ColumnId == 0).ToObservableCollection() });
            columnsCollection.Add(new Column() { Id=1, Name = "In Progress", Order = 1, WIP = 5, Cards = cards.Where(c => c.ColumnId == 1).ToObservableCollection() });
            columnsCollection.Add(new Column() { Id=2, Name = "Done", Order = 2, WIP = 0, Cards = cards.Where(c => c.ColumnId == 2).ToObservableCollection() });
            Columns = columnsCollection;
        }

        public ICommand AddColumn => new Command(() => Columns.Add(new Column() { Name="New column", WIP=0}));
        public ICommand AddCard => new Command<int>((columnId) => Columns.First(c=>c.Id == columnId).Cards.Add(new Card() { Name = "New card" }));
        public ICommand DeleteCard => new Command<int>((cardId) => cards.Remove(cards.First(c=>c.Id == cardId)));

        public ObservableCollection<Column> Columns
        {
            get => columns;
            set
            {
                columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }
    }



    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int ColumnId { get; set; }
        public Column Column { get; set; }
    }

    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WIP { get; set; }
        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();
        public int Order { get; set; }
    }

    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}
