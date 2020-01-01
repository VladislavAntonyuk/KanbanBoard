using Microsoft.EntityFrameworkCore;

namespace KanbanBoard
{
    public class ApplicationContext : DbContext
    {
        private string _databasePath;

        public DbSet<Column> Columns { get; set; }
        public DbSet<Card> Cards { get; set; }

        public ApplicationContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
