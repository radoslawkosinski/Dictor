using System.Collections.Generic;

namespace Dictor.Experiments
{
    public interface IMWResponseRaw
    {
        List<MWTranslationRaw> Translations { get; set; }
    }
}