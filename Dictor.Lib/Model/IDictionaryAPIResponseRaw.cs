using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Lib.Model
{
    public interface IDictionaryAPIResponseRaw 
    {
       public List<DictionaryAPIResponseRaw> Translations { get; set; }
    }
}
