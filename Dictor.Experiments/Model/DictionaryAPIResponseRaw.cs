using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Experiments.Model
{


    public class DictionaryAPIResponseRaw : IDictionaryAPIResponseRaw
    {
        [JsonProperty(Required = Required.AllowNull)]
        public List<DictionaryAPIResponseRaw> Translations { get; set; }

        public string Word { get; set; }

        public string Phonetic { get; set; }

        public string Origin { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public Meaning Meaning { get; set; }

        public DictionaryAPIResponseRaw()
        {
            this.Meaning = new Meaning();
        }
    }

    
    public partial class Meaning
    {
        public List<Noun> Noun { get; set; }
        public Meaning()
        {
            var noun = new Noun { Definition = string.Empty, Example = string.Empty};
            Noun = new List<Noun>() { noun};
        }
    }

    public partial class Noun
    {
        public string Definition { get; set; }
        public string Example { get; set; }
    }


}
