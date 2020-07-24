using System.Threading.Tasks;

namespace Dictor.Experiments
{
    public interface ITranslationProvider
    {
       public Task<TranslationResult> Translate(string phrase);

        public string ProviderName { get;  }


    }
}