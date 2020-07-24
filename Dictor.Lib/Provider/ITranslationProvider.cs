using Dictor.Lib.Model;
using System.Threading.Tasks;

namespace Dictor.Lib.Provider
{
    public interface ITranslationProvider
    {
       public Task<TranslationResult> Translate(string phrase);

        public string ProviderName { get;  }

        public Task ListenAudio(string phrase);
    }
}