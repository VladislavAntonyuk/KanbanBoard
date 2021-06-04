using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;

namespace KanbanBoard
{
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
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("FontAwesome5Solid.otf", "FASolid");
                });
        }
    }
}