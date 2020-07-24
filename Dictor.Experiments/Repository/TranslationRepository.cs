using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictor.Experiments
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

        public async Task<List<TranslationResult>> TranslateAllProviders(string phrase)
        {
            var results = new List<TranslationResult>();
            foreach (var prov in _translationProviders.Providers)
            {
                results.Add(await prov.Translate(phrase).ConfigureAwait(false));
            }
            return results;
        }

        public async Task<TranslationResult> TranslateProvider(string ProviderName, string phrase)
        {
            ITranslationProvider provider = _translationProviders.Providers.SingleOrDefault(s => s.ProviderName == ProviderName);


            return await provider.Translate(phrase).ConfigureAwait(false); ;
        }


    }
}
