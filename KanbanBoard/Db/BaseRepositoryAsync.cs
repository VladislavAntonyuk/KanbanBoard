using System.Collections.Generic;
using System.Threading.Tasks;
using Polly;
using SQLite;

namespace KanbanBoard.Db
{
    public abstract class BaseRepositoryAsync<T> : IBaseRepository<T> where T : new()
    {
        SQLiteAsyncConnection database;
        public BaseRepositoryAsync(IPath path)
        {
            var dbPath = path.GetDatabasePath();
            database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            database.CreateTableAsync<T>().Wait();
        }

        public Task<List<T>> GetItems()
        {
            return AttemptAndRetry(()=>database.Table<T>().ToListAsync());
        }

        public Task<T> GetItem(int id)
        {
            return AttemptAndRetry(()=>database.GetAsync<T>(id));
        }

        public Task DeleteItem(int id)
        {
            return AttemptAndRetry(()=>database.DeleteAsync<T>(id));
        }

        public async Task<T> UpdateItem(T item)
        {
            await AttemptAndRetry(()=>database.UpdateAsync(item));
            return item;
        }

        public async Task<T> SaveItem(T item)
        {
            await AttemptAndRetry(()=>database.InsertAsync(item));
            return item;
        }

        protected static Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 10)
        {
            return Policy.Handle<SQLite.SQLiteException>().WaitAndRetryAsync(numRetries, pollyRetryAttempt).ExecuteAsync(action);

            static TimeSpan pollyRetryAttempt(int attemptNumber) => TimeSpan.FromMilliseconds(Math.Pow(2, attemptNumber));
        }
    }
}