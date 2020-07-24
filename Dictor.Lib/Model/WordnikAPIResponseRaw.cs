using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Dictor.Lib.Model
{



    //public partial class WordnikAPIResponseRaw : IWordnikAPIResponseRaw
    //{
    //    public List<WordnikTranslationsRaw> Translations { get; set; }
    //}


    public partial class WordnikAPIResponseRaw : IWordnikAPIResponseRaw
    {
        [JsonProperty("examples")]
        public List<Example> Examples { get; set; }
    }

    public partial class Example
    {
        //[JsonProperty("provider")]
        //public Provider Provider { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("word")]
        public string Word { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("documentId")]
        public long DocumentId { get; set; }

        [JsonProperty("exampleId")]
        public long ExampleId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }
    }

    //public partial class Provider
    //{
    //    [JsonProperty("id")]
    //    public int Id { get; set; }
    //}

    

}

