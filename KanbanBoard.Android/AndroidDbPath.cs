using System;
using System.IO;
using KanbanBoard.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDbPath))]

namespace KanbanBoard.Droid
{
    public class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }
    }
}