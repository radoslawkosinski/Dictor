using Dictor.Lib.Model;
using Dictor.Lib.Provider;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictor.Lib.Repository
{
    public class TranslationRepository : ITranslationRepository
    {
        //public List<ITranslationProvider> Providers = new List<ITranslationProvider>();
        //private readonly AppSettings settings;

        public TranslationRepository(IOptions<AppSettings> _settings, TranslationProviders providers)
        {
            _translationProviders = providers;
        }

        private TranslationProviders _translationProviders { get; }

        public async Task ListenAudio(string providerName, string phrase)
        {
            ITranslationProvider provider = _translationProviders.Providers.SingleOrDefault(s => s.ProviderName == providerName);


            await provider.ListenAudio(phrase).ConfigureAwait(false); ;
        }

        public async Task<List<TranslationResult>> TranslateAllProviders(string phrase)
        {
            var results = new List<TranslationResult>();
            foreach (var prov in _translationProviders.Providers)
            {
                results.Add(await prov.Translate(phrase).ConfigureAwait(false));
            }
            return results;
        }

        public async Task<TranslationResult> TranslateProvider(string providerName, string phrase)
        {
            ITranslationProvider provider = _translationProviders.Providers.SingleOrDefault(s => s.ProviderName == providerName);


            return await provider.Translate(phrase).ConfigureAwait(false); ;
        }


    }
}
