using System.Linq;
using Xamarin.Forms;

namespace KanbanBoard
{
    public partial class App : Application
    {
        public const string DBFILENAME = "KanbanBoard.db";
        public static string dbPath;
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] {
                "SwipeView_Experimental",
                "IndicatorView_Experimental"
            });
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
            using (var db = new ApplicationContext(dbPath))
            {
                db.Database.EnsureCreated();                
            }
            AddTestData();
            MainPage = new MainPage();
        }

        private void AddTestData()
        {
            using (var db = new ApplicationContext(dbPath))
            {
                var todoColumn = new Column() { Name = "ToDo", Order = 1 };
                var inProgressColumn = new Column() { Name = "In Progress", Order = 2, WIP = 3 };
                if (db.Columns.Count() == 0)
                {
                    db.Columns.Add(todoColumn);
                    db.Columns.Add(inProgressColumn);
                    db.Columns.Add(new Column() { Name = "Done", Order = 3});

                    db.SaveChanges();
                }

                if (db.Cards.Count() == 0)
                {
                    db.Cards.Add(new Card() { Name = "Card 1", Description = "Description for card 1", Order = 1, Column = todoColumn });
                    db.Cards.Add(new Card() { Name = "Card 2", Description = "Description for card 2", Order = 2, Column = todoColumn });
                    db.Cards.Add(new Card() { Name = "Card 3", Description = "Description for card 3", Order = 1, Column = inProgressColumn });

                    db.SaveChanges();
                }
            }
        }
    }
}
