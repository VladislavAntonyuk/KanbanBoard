using KanbanBoard.Db;
using KanbanBoard.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Application = Microsoft.Maui.Controls.Application;
using System;

namespace KanbanBoard
{
    public partial class App : Application
    {
        private const string DbFileName = "KanbanBoard.db";
        public static string DbPath;
        private IServiceProvider serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            DbPath = serviceProvider.GetRequiredService<IPath>().GetDatabasePath(DbFileName);
            DbEnsureCreated();
            AddTestData();
			MainPage = new MainPage();
        }

        public static void DbEnsureCreated()
        {
            //using var db = new ApplicationContext(DbPath);
            //db.Database.EnsureCreated();
        }

        public static void DbEnsureDeleted()
        {
            //using var db = new ApplicationContext(DbPath);
            //db.Database.EnsureDeleted();
        }

        public void AddTestData()
        {
            var columnsRepository = serviceProvider.GetRequiredService<IColumnsRepository>();
            var cardsRepository = serviceProvider.GetRequiredService<ICardsRepository>();
            if (!cardsRepository.GetItems().Any())
            {
                var todoColumn = new Column { Name = "ToDo", Order = 1 };
                var inProgressColumn = new Column { Name = "In Progress", Order = 2, Wip = 3 };
                columnsRepository.SaveItem(todoColumn);
                columnsRepository.SaveItem(inProgressColumn);
                columnsRepository.SaveItem(new Column { Name = "Done", Order = 3 });

                cardsRepository.SaveItem(new Card
                { Name = "Card 1", Description = "Description for card 1", Order = 1, Column = todoColumn });
                cardsRepository.SaveItem(new Card
                { Name = "Card 2", Description = "Description for card 2", Order = 2, Column = todoColumn });
                cardsRepository.SaveItem(new Card
                {
                    Name = "Card 3",
                    Description = "Description for card 3",
                    Order = 1,
                    Column = inProgressColumn
                });
            }
        }
    }
}
