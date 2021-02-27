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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Column>()
			            .HasMany(x=>x.Cards)
						.WithOne(x => x.Column)
						.HasForeignKey(x => x.ColumnId)
						.OnDelete(DeleteBehavior.Cascade);
		}
	}
}