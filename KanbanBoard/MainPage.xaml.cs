using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KanbanBoard
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var todoCards = new ObservableCollection<Card>();
            todoCards.Add(new Card() { Name = "Card 1", Description = "Description for card 1", Order = 0 });
            todoCards.Add(new Card() { Name = "Card 2", Description = "Description for card 2", Order = 1 });
            var inProgressCards = new ObservableCollection<Card>();
            inProgressCards.Add(new Card() { Name = "Card 1", Description = "Description for card 1", Order = 0 });
            inProgressCards.Add(new Card() { Name = "Card 2", Description = "Description for card 2", Order = 1 });
            var doneCards = new ObservableCollection<Card>();
            doneCards.Add(new Card() { Name = "Card 1", Description = "Description for card 1", Order = 0 });
            doneCards.Add(new Card() { Name = "Card 2", Description = "Description for card 2", Order = 1 });
            var columnsCollection = new ObservableCollection<Column>();
            columnsCollection.Add(new Column() { Name = "ToDo", Order = 0, WIP = 0 , Cards=todoCards});
            columnsCollection.Add(new Column() { Name = "In Progress", Order = 1, WIP = 5, Cards = inProgressCards });
            columnsCollection.Add(new Column() { Name = "Done", Order = 2, WIP = 0, Cards = doneCards });
            Board.ItemsSource = columnsCollection;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {

        }
    }

    public class Card
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }

    public class Column
    {
        public string  Name { get; set; }
        public int WIP { get; set; }
        public IEnumerable<Card> Cards { get; set; } = new List<Card>();
        public int Order { get; set; }
    }
}
