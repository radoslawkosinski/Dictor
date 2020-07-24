using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dictor.Experiments
{
    public interface ITranslationRepository
    {
        public Task<List<TranslationResult>> TranslateAllProviders(string phrase);
        public Task<TranslationResult> TranslateProvider(string providerName, string phrase);
    }
}