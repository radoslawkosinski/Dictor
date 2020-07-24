using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dictor.Lib.Model
{



    public class MWResponseRaw : IMWResponseRaw
    {
        public List<MWTranslationRaw> Translations { get; set; }
    }

    public class MWTranslationRaw
    {
        public Meta meta { get; set; }
        public Hwi hwi { get; set; }
        public string fl { get; set; }
        public List<In> ins { get; set; }
        public List<Def> def { get; set; }
        public List<Dro> dros { get; set; }
        [JsonProperty("shortdef")]
        public List<string> ShortTranslation { get; set; }
        public string gram { get; set; }
        public int hom { get; set; }
        public List<Uro> uros { get; set; }
    }

    public class Meta
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string src { get; set; }
        public string section { get; set; }
        public Target target { get; set; }
        public string highlight { get; set; }
        public List<string> stems { get; set; }
        public AppShortdef appshortdef { get; set; }
        public bool offensive { get; set; }
    }

    public class Target
    {
        public string tuuid { get; set; }
        public string tsrc { get; set; }
    }

    public class AppShortdef
    {
        public string hw { get; set; }
        public string fl { get; set; }
        public List<string> def { get; set; }
    }

    public class Hwi
    {
        [JsonProperty("hw")]
        public string Word { get; set; }
        [JsonProperty("prs", NullValueHandling = NullValueHandling.Include)]
        public List<Pr> prs { get; set; }
        public List<Altpr> altprs { get; set; }
        public Hwi()
        {
            prs = new List<Pr>() { new Pr() };
        }
    }

    
    public class Pr
    {
        [DefaultValue("")]
        [JsonProperty(PropertyName = "ipa", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string ipa { get; set; }

        public Sound sound { get; set; }
        public Pr()
        {
            sound = new Sound();
        }
    }



    public class Altpr
    {
        public string ipa { get; set; }
    }

    public class In
    {
        public string il { get; set; }
        public string _if { get; set; }
        public string ifc { get; set; }
        public Pr1[] prs { get; set; }
    }

    public class Pr1
    {
        public string ipa { get; set; }
        public Sound1 sound { get; set; }
    }

    public class Sound1
    {
        public string audio { get; set; }
    }

    public class Def
    {
        public object[][][] sseq { get; set; }
    }

    public class Dro
    {
        public string drp { get; set; }
        public Def1[] def { get; set; }
    }

    public class Def1
    {
        public object[][][] sseq { get; set; }
    }

    public class Uro
    {
        public string ure { get; set; }
        public string fl { get; set; }
        public string gram { get; set; }
        public object[][] utxt { get; set; }
    }



}
