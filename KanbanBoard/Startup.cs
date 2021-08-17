using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using KanbanBoard.Db;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace KanbanBoard;

public class Startup : IStartup
{
    public void Configure(IAppHostBuilder appBuilder)
    {
        appBuilder
            .UseMauiApp<App>()
            .UseMauiServiceProviderFactory(constructorInjection: true)
            .ConfigureServices(services =>
            {
                services.AddTransient<IPath, DbPath>();
                services.AddTransient<IColumnsRepository, ColumnsRepository>();
                services.AddTransient<ICardsRepository, CardsRepository>();
                services.AddTransient<MainPageViewModel>();
                services.AddTransient<MainPage>();
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome5Solid.otf", "FASolid");
            });
    }
}
