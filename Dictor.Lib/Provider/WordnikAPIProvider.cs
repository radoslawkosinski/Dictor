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
    public class WordnikAPIProvider : IWordnikAPIProvider
    {
        private readonly AppSettings settings;

        private WordnikAPIResponseRaw translationResultRaw;

        public string ProviderName { get => "Wordnik";  }

        public List<Language> AvailableLanguages; //if empty then multiple languages not supported
        
        private int limit = 15;


        public WordnikAPIProvider(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
        {            
            this.settings = _settings.Value;
            LoadSupportedLanguages(); //load list of supported languages, that can be loaded from API if it is available
        }


        /// <summary>
        /// Use wordnik for example sentences only, the translation provided by wordnik doesn't seem to be good quality (missing many things)
        /// but the sample usage looks quite OK for our purposes
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
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



            string APIKey = settings.APIKeys.WordnikAPIKey;
            string req = $"https://api.wordnik.com/v4/word.json/{phrase}/examples?includeDuplicates=false&useCanonical=false&limit={limit}&api_key={APIKey}";

            //wordnik swagger
            //https:\/\/developer.wordnik.com\/api-docs\/swagger.json            
            //wordnik get word definitions

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
                    translationResultRaw = content.ToObject<WordnikAPIResponseRaw>();
                }
 
            }
            catch (WebException ex)
            {
                //TODO --ADD LOGGING
                Console.WriteLine(ex.Message);
            }


            //CONVERT TO FINAL RESPONSE
            return mapResponse();

        }



        private TranslationResult mapResponse()
        {
            TranslationResult translationResult = new TranslationResult(this.ProviderName);

            translationResult.Results.Add(new Result());



            //always single result (this is only the list<OnlineExample>
            translationResult.Results[0].OnlineExamples = translationResultRaw
                .Examples.Select(
                x => new OnlineExample()
                {
                    Author = x?.Author,
                    Text = x?.Text,
                    Title = x?.Title,
                    Url = x?.Url,
                    Word = x?.Word,
                    Year = x?.Year
                }
                )
                .Where(x => !string.IsNullOrEmpty(x.Title) || !string.IsNullOrEmpty(x.Text))?
                .ToList();
            return translationResult;
        }

        public Task ListenAudio(string phrase)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Load list of languages available for translation on this provider
        /// </summary>
        private void LoadSupportedLanguages()
        {
            AvailableLanguages = new List<Language>
            {
                //Languages.Eng,
                //Languages.Pl
            };
        }
    }
}
