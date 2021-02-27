namespace KanbanBoard
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public static class Extensions
	{
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
		{
			return new(col);
		}
	}
}