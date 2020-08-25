using Dictor.Lib.Helpers;
using Dictor.Lib.Mocks;
using Dictor.Lib.Model;
using Dictor.Lib.Provider;
using Dictor.Lib.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dictor.Lib
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddLibraryServices(this IServiceCollection collection)
        {

            //providers
           //AddProviderMocks();

           AddProviders();



            //responses:

            collection.AddTransient<IMWResponseRaw, MWResponseRaw>();



            collection.AddTransient<ITranslationRepository, TranslationRepository>();

            collection.AddTransient<ITranslationService, TranslationService>();

            collection.AddSingleton<TranslationProviders, TranslationProviders>();

            

            return collection;


            void AddProviderMocks()
            {
                collection.AddTransient<IMWProvider, MWProviderMock>();
                collection.AddTransient<IDictionaryAPIProvider, DictionaryAPIProviderMock>();
                collection.AddTransient<IWordnikAPIProvider, WordnikProviderMock>();
            }

            void AddProviders()
            {
                collection.AddTransient<IMWProvider, MWProvider>();
                collection.AddTransient<IDictionaryAPIProvider, DictionaryAPIProvider>();
                collection.AddTransient<IWordnikAPIProvider, WordnikAPIProvider>();
            }
        }


    }
}
