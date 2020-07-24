using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Lib.Model
{

    //public class  Translations{
    //    public List<TranslationResult> TranslationResults { get; set; };
    //    public Translations()
    //    {

    //    }
    //};

    public class TranslationResult
    {
        public List<Result> Results { get; set; }
        
        
        //public string Example { get; set; }
        public string ProviderName { get; set; }
        
        
        

        public TranslationResult(string providerName)
        {

            Results = new List<Result>();
            ProviderName = providerName;
        }
    }

    public class Result
    {
        public string Word { get; set; }
        public List<string> ShortTranslation { get; set; } //shortdef in mw
        public List<OnlineExample> OnlineExamples { get; set; }
        public List<TranslationDefinition> Definitions { get; set; }
        
        



        public Result()
        {
            Definitions = new List<TranslationDefinition>();
            

            ShortTranslation = new List<String>();
            OnlineExamples = new List<OnlineExample>();
            
        }
    }

    public class Pronounciation
    {
        public string Pron { get; set; }
        public Sound ? Sound { get; set; }
    }



    public class TranslationDefinition
        {
            public string Definition { get; set; }
            public string Example { get; set; }
            public List<Pronounciation> Pronounciations { get; set; }
        public List<Synonym> Synonyms { get; set; }
        public TranslationDefinition()
            {
                Pronounciations = new List<Pronounciation>();
                Synonyms = new List<Synonym>();
            }
        }
    /// <summary>
    /// that is currently populated by wordnik
    /// </summary>
    public class OnlineExample
    {
        public string Year { get; set; }
        public string Url { get; set; }
        public string Word { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }

    public class Synonym
    {
        public string Name { get; set; }
    }
}
