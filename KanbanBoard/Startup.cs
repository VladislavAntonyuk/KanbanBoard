using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using KanbanBoard.Db;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace KanbanBoard
{
    public class Startup : IStartup
    {
        public void Configure(IAppHostBuilder appBuilder)
        {
            appBuilder
                .UseMauiApp<App>()
                .UseMauiServiceProviderFactory(constructorInjection: true)
                .ConfigureMauiHandlers(handlers =>
                {
                    #if __ANDROID__
                        handlers.AddCompatibilityRenderer(typeof(Microsoft.Maui.Controls.IndicatorView), typeof(Microsoft.Maui.Controls.Compatibility.Platform.Android.IndicatorViewRenderer));
                    #endif
                })
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
}