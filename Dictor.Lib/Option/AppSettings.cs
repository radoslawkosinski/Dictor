using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Lib
{
    public class AppSettings


    {
        public string TempDirectory { get; set; }
        public Keys APIKeys
        {
            get; set;
        }

        /// <summary>
        /// keys required to API
        /// </summary>
        public class Keys
        {
            public string MWAPIKey { get; set; }
            public string WordnikAPIKey { get; set; }
        }
    }

}
