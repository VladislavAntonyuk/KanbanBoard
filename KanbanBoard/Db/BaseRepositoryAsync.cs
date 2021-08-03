using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace KanbanBoard.Db
{
    public abstract class BaseRepositoryAsync<T> : IBaseRepository<T> where T : new()
    {
        SQLiteAsyncConnection database;
        public BaseRepositoryAsync(IPath path)
        {
            var dbPath = path.GetDatabasePath();
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<T>().Wait();
        }

        public Task<List<T>> GetItems()
        {
            return database.Table<T>().ToListAsync();
        }

        public Task<T> GetItem(int id)
        {
            return database.GetAsync<T>(id);
        }

        public Task DeleteItem(int id)
        {
            return database.DeleteAsync<T>(id);
        }

        public async Task<T> UpdateItem(T item)
        {
            await database.UpdateAsync(item);
            return item;
        }

        public async Task<T> SaveItem(T item)
        {
            await database.InsertAsync(item);
            return item;
        }
    }
}