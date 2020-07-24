using Dictor.Experiments.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Dictor.Experiments
{
    public class DictionaryAPIProvider : ITranslationProvider
    {
        private readonly AppSettings settings;

        private List<DictionaryAPIResponseRaw> translationResultRaw;

        public string ProviderName { get => "DictionaryAPI";  }

        public List<Language> AvailableLanguages; //if empty then multiple languages not supported


        public DictionaryAPIProvider(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
        {            
            this.settings = _settings.Value;
            LoadSupportedLanguages(); //load list of supported languages, that can be loaded from API if it is available
        }



        private void mapResult()
        {
            throw new NotImplementedException();
        }

        public async Task<TranslationResult> Translate(string phrase)
        {
            //use below only if provider supports multiple langiages...
            Language sourceLang = Languages.SelectedTranslation.SourceLang;
            Language targetLang = Languages.SelectedTranslation.TargetLang;


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



            //string APIKey = settings.APIKeys.MWAPIKey;
            string req = $"https://api.dictionaryapi.dev/api/v1/entries/en/{phrase}";


            var client = new RestClient(req);

            var request = new RestRequest();

            request.Method = Method.GET;


            try
            {
                var response = await client.ExecuteAsync(request);

                Task<IRestResponse> t = client.ExecuteAsync(request);


                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    translationResultRaw = content.ToObject<List<DictionaryAPIResponseRaw>>();
                }
 
            }
            catch (WebException ex)
            {
                //TODO
                Console.WriteLine(ex.Message);
            }


            //CONVERT TO FINAL RESPONSE
            return mapResponse();

        }



        private TranslationResult mapResponse()
        {
            TranslationResult translationResult = new TranslationResult();
            translationResult.Word = translationResultRaw[0].Word;
            translationResult.ProviderName = this.ProviderName;
            translationResult.Definitions = translationResultRaw[0].Meaning.Noun
                .Select(x => new Definition() { Name = x.Definition, Example = x.Example })
                .ToList();
            translationResult.Translation = translationResultRaw[0].Meaning.Noun[0].Definition; //improve this. it should be the list<>
            //translationResult.Synonyms = translationResultRaw[0].

            return translationResult;

            //use linq to map
            //translationResultRaw[0].def[0].sseq.Rank; //etc
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


    }
}
