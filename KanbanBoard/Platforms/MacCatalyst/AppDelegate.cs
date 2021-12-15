using Foundation;
using SQLitePCL;

namespace KanbanBoard;

[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp()
    {
        raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
        return MauiProgram.CreateMauiApp();
    }
}
