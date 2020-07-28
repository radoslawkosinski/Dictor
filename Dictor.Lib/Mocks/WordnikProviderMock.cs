using Dictor.Lib.Model;
using Dictor.Lib.Provider;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dictor.Lib.Mocks
{
    public class WordnikProviderMock : IWordnikAPIProvider
    {
        private readonly AppSettings settings;

        private WordnikAPIResponseRaw translationResultRaw;
        private MockResponse response;

        public string ProviderName { get => "Wordnik"; }

        public List<Language> AvailableLanguages; //if empty then multiple languages not supported

        public WordnikProviderMock(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
        {
            this.settings = _settings.Value;
            this.response = new MockResponse();
        }



        public async Task<TranslationResult> Translate(string phrase)
        {

            var jt = JToken.Parse(response.GetWordnikAPIMockResponse()).ToString();

            translationResultRaw = JsonConvert.DeserializeObject<JToken>(JToken.Parse(response.GetWordnikAPIMockResponse()).ToString())
            .ToObject<WordnikAPIResponseRaw>();


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
    }
}

