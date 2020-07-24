using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dictor.Lib.Model
{
    public class Sound
    {
        [DefaultValue("abc")]
        [JsonProperty(PropertyName = "audio", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Audio { get; set; }
        public Sound()
        {
            Audio = "";
        }
    }
}
