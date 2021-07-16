using System;
using System.IO;

namespace KanbanBoard
{
    public class DbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library",
                sqliteFilename);
        }
    }
}