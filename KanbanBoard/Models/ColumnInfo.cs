namespace KanbanBoard.Models
{
	public class ColumnInfo
	{
		public ColumnInfo(Column column)
		{
			Column = column;
		}

		public Column Column { get; }

		public bool IsWipReached => Column.Cards.Count >= Column.Wip;
	}
}