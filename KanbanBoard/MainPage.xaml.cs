using CommunityToolkit.Maui.Alerts.Snackbar;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace KanbanBoard;

public partial class MainPage : ContentPage
{
    private readonly IPath path;

    public MainPage(MainPageViewModel viewModel, IPath path)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ResetButton ??= new();
        this.path = path;
    }

    private async void ResetButton_OnClicked(object sender, EventArgs e)
    {
        var options = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.Green,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(14),
        };
        await ResetButton.DisplaySnackbar(
            "All your data will be deleted in 3 seconds. Application will be closed",
            DeleteDbAndCloseApp,
            "Confirm and delete immediately",
            TimeSpan.FromSeconds(5),
            options);
    }

    private void DeleteDbAndCloseApp()
    {
        var dbPath = path.GetDatabasePath();
        path.DeleteFile(dbPath);
        Environment.Exit(0);
    }
}
