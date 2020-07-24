using Dictor.Lib.Model;
using Dictor.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dictor.Lib
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
            var ret = await _repository.TranslateProvider(providerName, phrase).ConfigureAwait(false);
            return ret;
        }

        public async Task<List<TranslationResult>> TranslateAllProviders(string phrase)
        {
            var ret = await _repository.TranslateAllProviders(phrase).ConfigureAwait(false); ;
            return ret;
        }

        /// <summary>
        /// Each provider has its own implementation of this method
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public Task ListenAudio(string providerName, string phrase)
        {
            throw new NotImplementedException();
        }
    }
}
