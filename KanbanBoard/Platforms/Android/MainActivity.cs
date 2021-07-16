using Android.App;
using Android.Content.PM;
using Microsoft.Maui;

namespace KanbanBoard
{
	[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, Label = "KanbanBoard", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : MauiAppCompatActivity
	{
	}
}