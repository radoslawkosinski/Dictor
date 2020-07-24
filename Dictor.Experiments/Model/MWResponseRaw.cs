using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Experiments
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
        public In[] ins { get; set; }
        public Def[] def { get; set; }
        public Dro[] dros { get; set; }
        public string[] shortdef { get; set; }
        public string gram { get; set; }
        public int hom { get; set; }
        public Uro[] uros { get; set; }
    }

    public class Meta
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string src { get; set; }
        public string section { get; set; }
        public Target target { get; set; }
        public string highlight { get; set; }
        public string[] stems { get; set; }
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
        public string[] def { get; set; }
    }

    public class Hwi
    {
        public string hw { get; set; }
        public Pr[] prs { get; set; }
        public Altpr[] altprs { get; set; }
    }

    public class Pr
    {
        public string ipa { get; set; }
        public Sound sound { get; set; }
    }

    public class Sound
    {
        public string audio { get; set; }
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
