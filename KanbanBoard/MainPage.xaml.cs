using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;

namespace KanbanBoard;

public partial class MainPage : ContentPage
{
    private readonly IPath path;

    public MainPage(MainPageViewModel viewModel, IPath path)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.path = path;
    }

    private async void ResetButton_OnClicked(object sender, EventArgs e)
    {
        /*
         * this doesn't work in MauiCompat
        var shouldCancel = await ResetButton.DisplaySnackBarAsync(new SnackBarOptions
        {
            BackgroundColor = Colors.Peru,
            Duration = TimeSpan.FromSeconds(3),
            MessageOptions = new MessageOptions
            {
                Message = "All your data will be deleted in 3 seconds. Application will be closed"
            },
            Actions = new List<SnackBarActionOptions>
            {
                new()
                {
                    BackgroundColor = Colors.Black,
                    ForegroundColor = Colors.Red,
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
                    BackgroundColor = Colors.Red,
                    ForegroundColor = Colors.Black,
                    Text = "Cancel",
                    Font = Font.SystemFontOfSize(20),
                    Padding = new Thickness(20)
                }
            }
        });
        if (!shouldCancel) 
        {
            DeleteDbAndCloseApp();
        }
    }

    private void DeleteDbAndCloseApp()
    {*/
        var dbPath = path.GetDatabasePath();
        path.DeleteFile(dbPath);
        Environment.Exit(0);
    }
}
