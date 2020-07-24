//using Dictor.Lib.Model;
//using Dictor.Lib.Provider;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Dictor.Lib.Mocks
//{
//    public class MWProviderMock : IMWProvider
//    {
//        private readonly AppSettings settings;

//        private List<MWTranslationRaw> translationResultRaw;
//        private MockResponse response;

//        public string ProviderName { get => "M-W"; }

//        public List<Language> AvailableLanguages; //if empty then multiple languages not supported

//        public MWProviderMock(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
//        {
//            this.settings = _settings.Value;
//            this.response = new MockResponse();
//        }



//        public async Task<TranslationResult> Translate(string phrase)
//        {


//            //Task<IRestResponse> t = client.ExecuteAsync(request);
//            //using (StreamReader sr = File.OpenText(jsonFilePath))
//            //StreamReader sr = File.OpenText(jsonFilePath);
//            //{
//            //        string json = await sr.ReadToEndAsync();
//                    var content = JsonConvert.DeserializeObject<JToken>(JToken.Parse(response.GetMWMockResponse()).ToString());
//                    translationResultRaw = content.ToObject<List<MWTranslationRaw>>();
//            //}

            
//            //CONVERT TO FINAL RESPONSE
//            return mapResponse();

//        }



//        private TranslationResult mapResponse()
//        {
//            TranslationResult translationResult = new TranslationResult(this.ProviderName);


//            //       translationResult.Results = translationResultRaw
//            //.Select(x => new Result() { Word = x.hwi.Word, ShortTranslation = x.ShortTranslation,
//            //    //jak to rozwalic?
//            //    Pronounciations = { new Pronounciation { Audio = x.hwi.prs
//            //    .FirstOrDefault().sound.audio, Pron = "zzz"/*x.hwi.prs[0].ipa*/ } }

//            //})
//            ////.Where(x => !string.IsNullOrEmpty(x.Example) || !string.IsNullOrEmpty(x.Definition))
//            //.ToList();


//            /**
//                        translationResult.Results = translationResultRaw
//.Select(x => new Result()
//{
//                Word = x.hwi.Word,
//                ShortTranslation = x.ShortTranslation,
//                //jak to rozwalic?
//                Pronounciations = 
//                { new Pronounciation { Sound = x.hwi.prs
//                     .Where
//                     (x => !string.IsNullOrEmpty(x.sound.audio))
//                     .FirstOrDefault()?.sound ?? x.hwi.prs.FirstOrDefault().sound //this was prepopulated in constructor so either first not empty element or just first one (it could be either populated or not)
//                     ,Pron = x.hwi.prs
//                     .Where (x => !string.IsNullOrEmpty(x.ipa)).FirstOrDefault()?.ipa
//             } }
//})
////.Where(x => !string.IsNullOrEmpty(x.Example) || !string.IsNullOrEmpty(x.Definition))
//.ToList();


//                        **/
//            translationResult.Results = translationResultRaw
//            .Select(x => new Result()
//            {
//            Word = x.hwi.Word,
//            ShortTranslation = x.ShortTranslation,
                
//                Definitions =
//                            { new TranslationDefinition {
//                                Definition = null,
//                                Example = null,
//                                Pronounciations =
//                                    { new Pronounciation { Sound = x.hwi.prs
//                                         .Where
//                                         (x => !string.IsNullOrEmpty(x.sound.audio))
//                                         .FirstOrDefault()?.sound ?? x.hwi.prs.FirstOrDefault().sound //this was prepopulated in constructor so either first not empty element or just first one (it could be either populated or not)
//                                         ,Pron = x.hwi.prs
//                                         .Where (x => !string.IsNullOrEmpty(x.ipa)).FirstOrDefault()?.ipa
//                                    } }

//                         } 
//                    }
//                })
//                .ToList();


//            //            var object3Query = object1List
//            //   .Select(o1 => o1.items
//            //     .Select(o2 => new object3()
//            //     {
//            //         id = o1.id,
//            //         name = o1.name,
//            //         ...,
//            //         price = o2.price
//            //     }
//            //));

//            //Pronounciations = { new Pronounciation { Audio = "zz"/**x.hwi.prs[0].sound.audio**/, Pron = "zzz"/*x.hwi.prs[0].ipa*/ } }
//            //translationResult.Word = translationResultRaw[0].hwi.hw; //test only
//            //translationResult.ProviderName = this.ProviderName;
//            //translationResult.ShortTranslation = translationResultRaw
//            //   .SelectMany(x => x.ShortTranslation)
//            //    //.Where(x => !string.IsNullOrEmpty(x) || !string.IsNullOrEmpty(x.Example))
//            //    .ToList();


//            return translationResult;
//        }
//    }
//}

