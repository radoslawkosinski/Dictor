using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dictor.Lib.Model
{


    public class DictionaryAPIResponseRaw : IDictionaryAPIResponseRaw
    {
        
        public List<DictionaryAPIResponseRaw> Translations { get; set; }

        public string Word { get; set; }

        public string Phonetic { get; set; }

        //"ShortTranslation"
        [JsonProperty("Origin")] 
        public string ShortTranslation { get; set; }

        public Meaning Meaning { get; set; }
    }

    public partial class Meaning
    {
        [JsonProperty("Noun", NullValueHandling = NullValueHandling.Include)]
        public List<Noun> Noun { get; set; }
        //public Meaning()
        //{
        //    Noun = new List<Noun>() { new Noun { Definition = null, Example = null } };
        //    Adjective = new List<Adjective>() { new Adjective { Definition = null, Example = null, Synonyms = new List<string>() } };
        //}

        [JsonProperty("Adjective")]
        public List<Adjective> Adjective { get; set; }

    }

    
    public partial class Noun
    {
        [JsonProperty("Definition", NullValueHandling = NullValueHandling.Include)]
        public string Definition { get; set; }
        [JsonProperty("synonyms", NullValueHandling = NullValueHandling.Include)]
        public List<string> Synonyms { get; set; }
        [JsonProperty("Example", NullValueHandling = NullValueHandling.Include)]
        public string Example { get; set; }
    }

    public partial class Adjective
    {
        [JsonProperty("definition")]
        public string Definition { get; set; }

        [JsonProperty("synonyms", NullValueHandling = NullValueHandling.Include)]
        public List<string> Synonyms { get; set; }

        [JsonProperty("Example", NullValueHandling = NullValueHandling.Include)]
        public string Example { get; set; }
    }







}
