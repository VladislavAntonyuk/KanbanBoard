using System.Collections.Generic;
using KanbanBoard.Models;
using SQLite;
 
namespace KanbanBoard.Db
{
    public class CardsRepository : ICardsRepository
    {
        SQLiteConnection database;
        public CardsRepository()
        {
            database = new SQLiteConnection(App.DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
            database.CreateTable<Card>();
        }
        public IEnumerable<Card> GetItems()
        {
            return database.Table<Card>().ToList();
        }
        public Card GetItem(int id)
        {
            return database.Get<Card>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Card>(id);
        }
        public int SaveItem(Card item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}