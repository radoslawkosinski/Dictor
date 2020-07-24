using Dictor.Lib;
using Dictor.UI.ViewModels;
using Dictor.UI.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using System;
using System.IO;
using System.Windows;

namespace Dictor.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

        public IConfiguration Configuration { get; private set; }

        //private readonly ILogger<App> _logger;
        //private readonly AppSettings _appSettings;

        //public static IHost AppHost;

        public static ITranslationService TranslationService;

        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()  // Use default settings
                                                //new HostBuilder()          // Initialize an empty HostBuilder
        .ConfigureAppConfiguration((context, builder) =>
        {
            // Add other configuration files...

        }).ConfigureServices((context, services) =>
        {
            ConfigureServices(services);
        })
        .ConfigureLogging(logging =>
        {
            // Add loggers...
        })
        .Build();


            TranslationService = (ITranslationService)host.Services.GetService(typeof(ITranslationService));
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            services.AddOptions();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            //register window in splat
            Locator.CurrentMutable.RegisterLazySingleton(() => new MainWindow(), typeof(IViewFor<MainWindowViewModel>));

            services.AddLibraryServices();
        }
    }
}
