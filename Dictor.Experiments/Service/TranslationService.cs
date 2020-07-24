using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dictor.Experiments
{
   public class TranslationService : ITranslationService
    {
        private ITranslationRepository _repository { get; }
        public TranslationService(ITranslationRepository repository)
        {
            _repository = repository;
        }

        public async Task<TranslationResult> TranslateProvider(string providerName, string phrase)
        {
            var ret = await _repository.TranslateProvider(providerName, phrase).ConfigureAwait(false); ;
            return ret;
        }

        public async Task<List<TranslationResult>> TranslateAllProviders(string phrase)
        {
            var ret = await _repository.TranslateAllProviders(phrase).ConfigureAwait(false); ;
            return ret;
        }
    }
}
