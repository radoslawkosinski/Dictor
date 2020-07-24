using Dictor.Lib.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dictor.Lib.Repository
{
    public interface ITranslationRepository
    {
        public Task<List<TranslationResult>> TranslateAllProviders(string phrase);
        public Task<TranslationResult> TranslateProvider(string providerName, string phrase);
        public Task ListenAudio(string providerName, string phrase);
    }
}