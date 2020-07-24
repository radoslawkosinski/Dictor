using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Dictor.Experiments
{
    class Program
    {
        static string environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

        public static IServiceProvider ServiceProvider { get; private set; }


        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();



            ConfigureServices(services);


            // create service provider


            var serviceProvider = services.BuildServiceProvider();
            // entry to run app
            await serviceProvider.GetService<App>().Run();


        }

        public Program()
        {

        }

        

        private static void ConfigureServices(IServiceCollection services)
        {

            



            services.AddLogging(); //HOW TO CONFIGURELOGGING??????????????????
            // build config

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            services.AddOptions();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));


            // add services:

            services.AddTransient<MeriamWebsterExperimentalProvider, MeriamWebsterExperimentalProvider>();

            services.AddTransient<DictionaryAPIProvider, DictionaryAPIProvider>();

            services.AddTransient<IMWResponseRaw, MWResponseRaw>();

            services.AddTransient<ITranslationRepository, TranslationRepository>();

            services.AddTransient<ITranslationService, TranslationService>();

            services.AddSingleton<TranslationProviders, TranslationProviders>();



            // add app

            services.AddTransient<App>();
        }


    }





}

