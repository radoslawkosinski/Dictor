using Dictor.Lib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Dictor.Lib.Provider
{
    public class MWProvider : IMWProvider
    {
        private readonly AppSettings settings;

        private List<MWTranslationRaw> translationResultRaw;

        public string ProviderName { get => "M-W";  }

        public List<Language> AvailableLanguages; //if empty then multiple languages not supported


        public MWProvider(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
        {            
            this.settings = _settings.Value;
            LoadSupportedLanguages(); //load list of supported languages, that can be loaded from API if it is available
        }



        public async Task<TranslationResult> Translate(string phrase)
        {
            //use below only if provider supports multiple langiages...
            Language sourceLang = Languages.SelectedTranslation.SourceLang;
            Language targetLang = Languages.SelectedTranslation.TargetLang;


            //List<string> lst = new List<string> { "bsss", "kuku", "abe" };
            //bool supportMultiLang = !AvailableLanguages.Any();
            //check if passed source lang(static) is in the supported lang list, if not, choose default pair from the list (first and second in the list)
            //or skip that part if there are no support for multiple languages
            //we assume that there can be any pair of supported (source - target languages), only restriction is both must be supported
            if (AvailableLanguages.Any())
            {
                if (!AvailableLanguages.Contains(sourceLang))
                    sourceLang = AvailableLanguages.FirstOrDefault<Language>();
                if (!AvailableLanguages.Contains(targetLang))
                    //TODO: check if the list contain 2 items, if not then take the first one
                    targetLang = AvailableLanguages[1];
            }



            string APIKey = settings.APIKeys.MWAPIKey;
            string req = $"https://www.dictionaryapi.com/api/v3/references/learners/json/{phrase}?key={APIKey}";


            var client = new RestClient(req);

            var request = new RestRequest();

            request.Method = Method.GET;




                var response = await client.ExecuteAsync(request);

                Task<IRestResponse> t = client.ExecuteAsync(request);


                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    translationResultRaw = (content.ToString().Contains("uuid")) ? content.ToObject<List<MWTranslationRaw>>() : null;

                }
 
            



            //CONVERT TO FINAL RESPONSE
            return mapResponse();

        }



        private TranslationResult mapResponse()
        {
            
            TranslationResult translationResult = new TranslationResult(this.ProviderName);

            if (translationResultRaw != null)
            {
                translationResult.Results = translationResultRaw?
                .Select(x => new Result()
                {
                    Word = null, //to be removed
                    ShortTranslation = null, //to be removed


                    Definitions =
                                { new TranslationDefinition {
                                Definition = x.hwi.Word,
                                Example = x.ShortTranslation.FirstOrDefault(),
                                Pronounciations =
                                    { new Pronounciation { Sound = x.hwi.prs
                                         .Where
                                         (x => !string.IsNullOrEmpty(x.sound?.Audio))
                                         .FirstOrDefault()?.sound ?? x.hwi?.prs?.FirstOrDefault().sound //this was prepopulated in constructor so either first not empty element or just first one (it could be either populated or not)
                                         ,Pron = x.hwi.prs
                                         .Where (x => !string.IsNullOrEmpty(x.ipa)).FirstOrDefault()?.ipa
                                    } }

                         }
                        }
                })
                    .ToList();
            }
            else translationResult = translationResult.GetEmptyTranslationResult();



            return translationResult;
        }

        /// <summary>
        /// Load list of languages available for translation on this provider
        /// </summary>
        private void LoadSupportedLanguages()
        {
            AvailableLanguages = new List<Language> { 
               //Languages.Eng,
               //Languages.Pl
            };
        }

        public Task ListenAudio(string phrase)
        {
            throw new NotImplementedException();
        }

        //private List<MWTranslationRaw> CreateEmptyResponse()
        //{
        //    return new List<MWTranslationRaw> { new MWTranslationRaw { ShortTranslation= new List<string>(), def = new List<Def>(), dros = new List<Dro>(), fl = null, gram = null, hom = 0, hwi = new Hwi { altprs = new List<Altpr>(), prs = new List<Pr> { new Pr {  ipa="", sound=new Sound {  Audio=""} } }, Word = "" } } };
        //}



    }
}
