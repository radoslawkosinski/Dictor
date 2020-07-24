using System.Collections.Generic;

namespace Dictor.Lib.Model
{
    public interface IMWResponseRaw
    {
        List<MWTranslationRaw> Translations { get; set; }
    }
}