using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace KanbanBoard
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ResetButton_OnClicked(object sender, EventArgs e)
        {
            var shouldCancel = await ResetButton.DisplaySnackBarAsync(new SnackBarOptions
            {
                BackgroundColor = Color.Peru,
                Duration = TimeSpan.FromSeconds(3),
                MessageOptions = new MessageOptions
                {
                    Message = "All your data will be deleted in 3 seconds. Application will be closed"
                },
                Actions = new List<SnackBarActionOptions>
                {
                    new()
                    {
                        BackgroundColor = Color.Black,
                        ForegroundColor = Color.Red,
                        Text = "Confirm and delete immediately",
                        Font = Font.SystemFontOfSize(20),
                        Padding = new Thickness(20),
                        Action = () =>
                        {
                            DeleteDbAndCloseApp();
                            return Task.CompletedTask;
                        }
                    },
                    new()
                    {
                        BackgroundColor = Color.Red,
                        ForegroundColor = Color.Black,
                        Text = "Cancel",
                        Font = Font.SystemFontOfSize(20),
                        Padding = new Thickness(20)
                    }
                }
            });
            if (!shouldCancel) DeleteDbAndCloseApp();
        }

        private static void DeleteDbAndCloseApp()
        {
            App.DbEnsureDeleted();
            Environment.Exit(0);
        }
    }
}