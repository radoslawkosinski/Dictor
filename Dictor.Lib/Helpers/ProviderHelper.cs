using Dictor.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Dictor.Lib.Helpers
{
    public static class ProviderHelper
    {
      public  static void CountResults(TranslationResult result)
        {
            result.OnlineExamplesCount = result.Results.Count > 0 ? result.Results.Select(x => x.OnlineExamples).FirstOrDefault().ToList().Count() : 0;
            result.DefinitionsCount = result.Results.Count > 0 ? result.Results.Where(x => x.Definitions.Count() > 0 ).Count() : 0;

            //var zz = result.Results.Select(x => x.OnlineExamples).FirstOrDefault().ToList();

            //var kuku = result.Results
            //.Select(
            // x => x.Definitions?
            //.Where(x => x.Definition == null
            //).Any()
            ////.Where(x => !string.IsNullOrEmpty(x.Example) )           

            //).ToList();




            //var blabal = result.Results
            //    .Select(x => x.Definitions).ToList()
            //    ;

            //var zxx = blabal.Select(x => x.Where(x => !string.IsNullOrEmpty(x.Definition))).ToList();



        }      
    }
}
