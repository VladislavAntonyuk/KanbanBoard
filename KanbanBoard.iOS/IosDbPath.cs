using KanbanBoard.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosDbPath))]

namespace KanbanBoard.iOS
{
	using System;
	using System.IO;

	public class IosDbPath : IPath
	{
		public string GetDatabasePath(string sqliteFilename)
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library",
				sqliteFilename);
		}
	}
}