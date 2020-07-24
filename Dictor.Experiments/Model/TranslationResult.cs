using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Experiments
{
    public class TranslationResult
    {
        public string Word { get; set; }
        public string Translation { get; set; }
        public string Example { get; set; }
        public string ProviderName { get; set; }
        public List<Synonym> Synonyms { get; set; }
        public List<Definition> Definitions { get; set; }
    }

       public class Definition
        {
            public string Name { get; set; }
            public string Example { get; set; }
        }

    public class Synonym
    {
        public string Name { get; set; }
    }
}
