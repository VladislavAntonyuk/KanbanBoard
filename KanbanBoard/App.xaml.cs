using System.Linq;
using KanbanBoard.Db;
using KanbanBoard.Models;
using Xamarin.Forms;

namespace KanbanBoard
{
    public partial class App
    {
        private const string DbFileName = "KanbanBoard.db";
        public static string DbPath;

        public App()
        {
            InitializeComponent();
            DbPath = DependencyService.Get<IPath>().GetDatabasePath(DbFileName);
            DbEnsureCreated();
            AddTestData();
            MainPage = new MainPage();
        }

        public static void DbEnsureCreated()
        {
            using var db = new ApplicationContext(DbPath);
            db.Database.EnsureCreated();
        }

        public static void DbEnsureDeleted()
        {
            using var db = new ApplicationContext(DbPath);
            db.Database.EnsureDeleted();
        }

        public static void AddTestData()
        {
            using var db = new ApplicationContext(DbPath);
            if (!db.Columns.Any())
            {
                var todoColumn = new Column {Name = "ToDo", Order = 1};
                var inProgressColumn = new Column {Name = "In Progress", Order = 2, Wip = 3};
                db.Columns.Add(todoColumn);
                db.Columns.Add(inProgressColumn);
                db.Columns.Add(new Column {Name = "Done", Order = 3});

                db.SaveChanges();

                db.Cards.Add(new Card
                    {Name = "Card 1", Description = "Description for card 1", Order = 1, Column = todoColumn});
                db.Cards.Add(new Card
                    {Name = "Card 2", Description = "Description for card 2", Order = 2, Column = todoColumn});
                db.Cards.Add(new Card
                {
                    Name = "Card 3",
                    Description = "Description for card 3",
                    Order = 1,
                    Column = inProgressColumn
                });

                db.SaveChanges();
            }
        }
    }
}