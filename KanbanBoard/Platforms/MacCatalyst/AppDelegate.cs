using Foundation;
using Microsoft.Maui;

namespace KanbanBoard
{
    [Register(nameof(AppDelegate))]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
	}
}