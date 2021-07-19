using System.Collections.Generic;
using KanbanBoard.Models;
using SQLite;
 
namespace KanbanBoard.Db
{
    public class ColumnsRepository : IColumnsRepository
    {
        SQLiteConnection database;
        public ColumnsRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Column>();
        }
        public IEnumerable<Column> GetItems()
        {
            return database.Table<Column>().ToList();
        }

        public Column GetItem(int id)
        {
            return database.Get<Column>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Column>(id);
        }
        public int SaveItem(Column item)
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