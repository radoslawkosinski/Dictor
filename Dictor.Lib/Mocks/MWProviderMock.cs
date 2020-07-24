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
    public class MWProviderMock : IMWProvider
    {
        private readonly AppSettings settings;

        private List<MWTranslationRaw> translationResultRaw;
        private MockResponse response;

        public string ProviderName { get => "M-W"; }

        public List<Language> AvailableLanguages; //if empty then multiple languages not supported

        public MWProviderMock(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
        {
            this.settings = _settings.Value;
            this.response = new MockResponse();
        }

        public async Task ListenAudio(string phrase)
        {
            throw new NotImplementedException();
        }

        public async Task<TranslationResult> Translate(string phrase)
        {


            //Task<IRestResponse> t = client.ExecuteAsync(request);
            //using (StreamReader sr = File.OpenText(jsonFilePath))
            //StreamReader sr = File.OpenText(jsonFilePath);
            //{
            //        string json = await sr.ReadToEndAsync();
                    var content = JsonConvert.DeserializeObject<JToken>(JToken.Parse(response.GetMWMockResponse()).ToString());
                    translationResultRaw = content.ToObject<List<MWTranslationRaw>>();
            //}

            
            //CONVERT TO FINAL RESPONSE
            return mapResponse();

        }



        private TranslationResult mapResponse()
        {
            TranslationResult translationResult = new TranslationResult(this.ProviderName);


            translationResult.Results = translationResultRaw
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
                                         (x => !string.IsNullOrEmpty(x.sound.Audio))
                                         .FirstOrDefault()?.sound ?? x.hwi.prs.FirstOrDefault().sound //this was prepopulated in constructor so either first not empty element or just first one (it could be either populated or not)
                                         ,Pron = x.hwi.prs
                                         .Where (x => !string.IsNullOrEmpty(x.ipa)).FirstOrDefault()?.ipa
                                    } }

                         } 
                    }
                })
                .ToList();




            return translationResult;
        }
    }
}

