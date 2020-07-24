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
    public class DictionaryAPIProvider : IDictionaryAPIProvider
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
                    //DictionaryAPIResponseRaw ra = new DictionaryAPIResponseRaw();
                    //translationResultRaw = new List<DictionaryAPIResponseRaw>();
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


        /// <summary>
        /// depends on the phrase, the JSON 'meaning' response can contain either 'adjective' or  'noun' so both should be mapped
        /// </summary>
        /// <returns></returns>
        private TranslationResult mapResponse()
        {

            TranslationResult translationResult = new TranslationResult(this.ProviderName);
            var definitionsFromNoun = translationResultRaw[0].Meaning.Noun
                .Select(x => new TranslationDefinition() { Definition = x.Definition, Example = x.Example })
                .Where(x => !string.IsNullOrEmpty(x.Example) || !string.IsNullOrEmpty(x.Definition))
                .ToList();

            var definitionsFromAdjective = translationResultRaw[0].Meaning.Adjective
                .Select(x => new TranslationDefinition() {  Definition = x.Definition, Example = x.Example})
                .Where(x => !string.IsNullOrEmpty(x.Definition) || !string.IsNullOrEmpty(x.Example))
                .ToList();


       /*     
            translationResult.Word = translationResultRaw[0].Word;
            translationResult.ProviderName = this.ProviderName;
            translationResult.ShortTranslation.Add(translationResultRaw.First().ShortTranslation);
                //[0].Origin;


            translationResult.Definitions = definitionsFromNoun;
            translationResult.Definitions.AddRange(definitionsFromAdjective);
 */

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
    }
}
