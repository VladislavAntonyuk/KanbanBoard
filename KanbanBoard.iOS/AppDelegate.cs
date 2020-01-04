namespace KanbanBoard.iOS
{
	using Foundation;
	using SQLitePCL;
	using UIKit;
	using Xamarin.Forms;
	using Xamarin.Forms.Platform.iOS;

	[Register(nameof(AppDelegate))]
	public class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Batteries_V2.Init();
			Forms.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}