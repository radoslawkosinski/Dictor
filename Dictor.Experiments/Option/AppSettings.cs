using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Experiments
{
    public class AppSettings


    {
        public string TempDirectory { get; set; }
        public Keys APIKeys
        {
            get; set;
        }

        public class Keys
        {
            public string MWAPIKey { get; set; }

        }
    }

}
