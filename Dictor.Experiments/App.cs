using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dictor.Experiments
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly AppSettings _appSettings;
        public App(IOptions<AppSettings> appSettings, ILogger<App> logger, ITranslationService ser)

        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));



            var t = Task.Run(() => ser.TranslateAllProviders("apple"));
            t.Wait();
        }

        public async Task Run()


        {
            await Task.CompletedTask;
        }
    }

}




