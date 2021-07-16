using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace KanbanBoard
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void ResetButton_OnClicked(object sender, EventArgs e)
        {
            //var shouldCancel = await ResetButton.DisplaySnackBarAsync(new SnackBarOptions
            //{
            //    BackgroundColor = Color.Peru,
            //    Duration = TimeSpan.FromSeconds(3),
            //    MessageOptions = new MessageOptions
            //    {
            //        Message = "All your data will be deleted in 3 seconds. Application will be closed"
            //    },
            //    Actions = new List<SnackBarActionOptions>
            //    {
            //        new()
            //        {
            //            BackgroundColor = Color.Black,
            //            ForegroundColor = Color.Red,
            //            Text = "Confirm and delete immediately",
            //            Font = Font.SystemFontOfSize(20),
            //            Padding = new Thickness(20),
            //            Action = () =>
            //            {
            //                DeleteDbAndCloseApp();
            //                return Task.CompletedTask;
            //            }
            //        },
            //        new()
            //        {
            //            BackgroundColor = Color.Red,
            //            ForegroundColor = Color.Black,
            //            Text = "Cancel",
            //            Font = Font.SystemFontOfSize(20),
            //            Padding = new Thickness(20)
            //        }
            //    }
            //});
            //if (!shouldCancel) DeleteDbAndCloseApp();
        }

        private static void DeleteDbAndCloseApp()
        {
            App.DbEnsureDeleted();
            Environment.Exit(0);
        }
    }
}
