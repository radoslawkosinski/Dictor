using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Lib.Provider
{
    public class TranslationProviders
    {
        public List<ITranslationProvider> Providers = new List<ITranslationProvider>();

        public TranslationProviders(IServiceProvider serviceProvider) 
        {
            //add all providers here
            //var MWProvider = serviceProvider.GetService<MeriamWebsterExperimentalProvider>();
            Providers.Add(serviceProvider.GetService<IWordnikAPIProvider>());
            Providers.Add(serviceProvider.GetService<IDictionaryAPIProvider>());
            Providers.Add(serviceProvider.GetService<IMWProvider>());
            
            //
        }
    }
}
