using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dictor.Experiments
{
    public interface ITranslationService
    {
        public Task<TranslationResult> TranslateProvider(string providerName, string phrase);
        public Task<List<TranslationResult>>  TranslateAllProviders(string phrase);
    }
}