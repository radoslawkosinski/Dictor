using Dictor.Lib.Helpers;
using Dictor.Lib.Model;
using Dictor.Lib.Provider;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dictor.Lib.Mocks
{
    public class DictionaryAPIProviderMock : IDictionaryAPIProvider
    {
        private readonly AppSettings settings;

        private List<DictionaryAPIResponseRaw> translationResultRaw;
        private MockResponse response;

        public string ProviderName { get => "DictionaryAPI"; }

        public List<Language> AvailableLanguages; //if empty then multiple languages not supported

        //private ProviderHelper providerHelper = new ProviderHelper();

        public DictionaryAPIProviderMock(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
        {
            this.settings = _settings.Value;
            this.response = new MockResponse();
        }



        public async Task<TranslationResult> Translate(string phrase)
        {



                    var content = JsonConvert.DeserializeObject<JToken>(JToken.Parse(response.GetDictionaryAPIMockResponseApple()).ToString());
                    translationResultRaw = content.ToObject<List<DictionaryAPIResponseRaw>>();

            
            //CONVERT TO FINAL RESPONSE
            return mapResponse();

        }

        /*
         https://stackoverflow.com/questions/15616932/convert-string-array-to-custom-object-list-using-linq
             */


        /// <summary>
        /// depends on the phrase, the JSON 'meaning' response can contain either 'adjective' or  'noun' so both should be mapped
        /// </summary>
        /// <returns></returns>
        private TranslationResult mapResponse()
        { 
            TranslationResult translationResult = new TranslationResult(this.ProviderName);

            var adj = translationResultRaw?.Select(x => x.Meaning.Adjective).FirstOrDefault()?.ToList();
            var noun = translationResultRaw?.Select(x => x.Meaning.Noun).FirstOrDefault()?.ToList();

            var res = new List<Result>();

            if (translationResultRaw != null)
            {
                if (adj != null)
                {
                    res = adj
                        .Select(x => new Result()
                        {
                            Definitions = {
                     new TranslationDefinition{
                     Definition = x.Definition,
                      Pronounciations = null,
                      Synonyms =  x.Synonyms?.Select(item => new Synonym() { Name = item })?.ToList(), //convert list<string> to list of new objects!!!!
                     Example = x.Example
                     }
                        }
                        })?.ToList();
                }

                if (noun != null)
                {
                    res = noun
                        .Select(x => new Result()
                        {
                            Definitions = {
                     new TranslationDefinition{
                     Definition = x.Definition,
                      Pronounciations = null,
                      Synonyms =  x.Synonyms?.Select(item => new Synonym() { Name = item })?.ToList(), //convert list<string> to list of new objects!!!!
                      Example = x.Example
                     }
                            }
                        })?.ToList();
                }
                translationResult.Results = res;
            }
            else
                translationResult = translationResult.GetEmptyTranslationResult();



            ProviderHelper.CountResults(translationResult);

            return translationResult;
        }
        public Task ListenAudio(string phrase)
        {
            throw new NotImplementedException();
        }
    }
}

