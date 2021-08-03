using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace KanbanBoard.Db
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : new()
    {
        SQLiteConnection database;

        public BaseRepository(IPath path)
        {
            var dbPath = path.GetDatabasePath();
            database = new SQLiteConnection(dbPath);
            database.CreateTable<T>();
        }

        public Task<List<T>> GetItems()
        {
            return Task.FromResult(database.Table<T>().ToList());
        }

        public Task<T> GetItem(int id)
        {
            return Task.FromResult(database.Get<T>(id));
        }

        public Task DeleteItem(int id)
        {
            database.Delete<T>(id);
            return Task.CompletedTask;
        }

        public Task<T> UpdateItem(T item)
        {
            database.Update(item);
            return Task.FromResult(item);
        }

        public Task<T> SaveItem(T item)
        {
            database.Insert(item);
            return Task.FromResult(item);
        }
    }
}