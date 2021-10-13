using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace KanbanBoard
{
    [Register(nameof(AppDelegate))]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
	}
}