using System;
using System.Collections.Generic;
using System.Text;

namespace Dictor.Experiments.Model
{
    public static class Languages 
    {
        public static readonly Language Eng =  new Language { LanguageDescription = "English", LanguageId = "eng" } ;
        public static readonly Language Pl = new Language { LanguageDescription = "Polish", LanguageId = "pl" };
        public static readonly Language Ger = new Language { LanguageDescription = "German", LanguageId = "ger" };
        public static readonly Language Es = new Language { LanguageDescription = "Spanish", LanguageId = "es" };
        public static readonly Language Ru = new Language { LanguageDescription = "Russian", LanguageId = "ru" };


        /// <summary>
        /// Globally set translation direction (iex. Eng - Ger etc
        /// </summary>
        public static class SelectedTranslation
        {
            public static Language SourceLang { get; set; } = Eng;
            public static Language TargetLang { get; set; } = Pl;
        }
    }
}
