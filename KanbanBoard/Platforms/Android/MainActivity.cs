using Android.Content.PM;
using Microsoft.Maui;

namespace KanbanBoard
{
    [Activity(
		Theme = "@style/Maui.SplashTheme",
		MainLauncher = true,
		Label = "KanbanBoard",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : MauiAppCompatActivity
	{
	}
}