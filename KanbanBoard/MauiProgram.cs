using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using KanbanBoard.Db;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace KanbanBoard;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome5Solid.otf", "FASolid");
            });
            builder.Services.AddTransient<IPath, DbPath>();
            builder.Services.AddTransient<IColumnsRepository, ColumnsRepository>();
            builder.Services.AddTransient<ICardsRepository, CardsRepository>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();
        return builder.Build();
    }
}
