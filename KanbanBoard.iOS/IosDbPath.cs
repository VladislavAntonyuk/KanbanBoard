using System;
using System.IO;
using KanbanBoard.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosDbPath))]
namespace KanbanBoard.iOS
{
    public class IosDbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", sqliteFilename);
        }
    }
}
