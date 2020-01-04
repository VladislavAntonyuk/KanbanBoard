namespace KanbanBoard.Db
{
	using Microsoft.EntityFrameworkCore;
	using Models;

	public class ApplicationContext : DbContext
	{
		private readonly string _databasePath;

		public ApplicationContext(string databasePath)
		{
			_databasePath = databasePath;
		}

		public DbSet<Column> Columns { get; set; }
		public DbSet<Card> Cards { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Filename={_databasePath}");
		}
	}
}