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

        public DictionaryAPIProviderMock(Microsoft.Extensions.Options.IOptions<AppSettings> _settings)
        {
            this.settings = _settings.Value;
            this.response = new MockResponse();
        }



        public async Task<TranslationResult> Translate(string phrase)
        {


            //Task<IRestResponse> t = client.ExecuteAsync(request);
            //using (StreamReader sr = File.OpenText(jsonFilePath))
            //StreamReader sr = File.OpenText(jsonFilePath);
            //{
            //        string json = await sr.ReadToEndAsync();
                    var content = JsonConvert.DeserializeObject<JToken>(JToken.Parse(response.GetDictionaryAPIMockResponseApple()).ToString());
                    translationResultRaw = content.ToObject<List<DictionaryAPIResponseRaw>>();
            //}

            
            //CONVERT TO FINAL RESPONSE
            return mapResponse();

        }

        /*
         https://stackoverflow.com/questions/15616932/convert-string-array-to-custom-object-list-using-linq
             */


        private TranslationResult mapResponse()
        {
            TranslationResult translationResult = new TranslationResult(this.ProviderName);

            var adj = translationResultRaw.Select(x => x.Meaning.Adjective).FirstOrDefault()?.ToList();
            var noun = translationResultRaw.Select(x => x.Meaning.Noun).FirstOrDefault()?.ToList();

            var res = new List<Result>();

            if (adj != null)
            {
                res = adj
                    .Select(x => new Result()
                    {
                        Definitions = {
                     new TranslationDefinition{
                     Definition = x.Definition,
                      Pronounciations = null,
                      Synonyms =  x.Synonyms.Select(item => new Synonym() { Name = item })?.ToList(), //convert list<string> to list of new objects!!!!
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

            /*
             string[] randomArray = new string[3] {"1", "2", "3"};
             List<CustomEntity> listOfEntities = randomArray.Select(item => new CustomEntity() { FileName = item } ).ToList();
            */

            /*
                List<test> tests = new List<test>();
                var ob1 = new test { obj1 = "obj1" };
                var ob2 = new test { obj1 = "obj2" };
                var ob3 = new test { obj1 = "obj3" };
                var ob4 = new test { obj1 = null };
                tests.Add(ob1);
                tests.Add(ob2);
                tests.Add(ob3);
                tests.Add(ob4);

                var result = tests.Select(e => new NewType
                {
                    name = e.obj1 != null ? e.obj1.ToString() : null
                });

                foreach (var item in result)
                {
                    Console.WriteLine(item.name);
                }
            */

            //translationResult.Results = translationResultRaw
            //.Select(x => new Result()
            //{
            //    Word = x.Word,
            //    ShortTranslation = { x.ShortTranslation },
            //    Definitions = { 
            //        new TranslationDefinition { 
            //        Definition = x.Meaning.Adjective
            //        .Select(x => x.Definition).FirstOrDefault(),
            //        Pronounciations = null,
            //        Synonyms = { new Synonym {
            //        Name = x.Meaning.Adjective
            //                .Where (x => !string.IsNullOrEmpty(x.Synonyms.ToString())).FirstOrDefault()?.ToString()
            //    } }
            //    } }
            //})
//.Where(x => !string.IsNullOrEmpty(x.Example) || !string.IsNullOrEmpty(x.Definition))
//.ToList();


            /*
                                             Pronounciations =
                                                { new Pronounciation { Sound = x.hwi.prs
                                                     .Where
                                                     (x => !string.IsNullOrEmpty(x.sound.audio))
                                                     .FirstOrDefault()?.sound ?? x.hwi.prs.FirstOrDefault().sound //this was prepopulated in constructor so either first not empty element or just first one (it could be either populated or not)
                                                     ,Pron = x.hwi.prs
                                                     .Where (x => !string.IsNullOrEmpty(x.ipa)).FirstOrDefault()?.ipa
                                                } }


                         */




            //var definitionsFromNoun = 
            //translationResultRaw[0].Meaning.Noun
            //    .Select(x => new TranslationDefinition() { Definition = x.Definition, Example = x.Example })
            //    .Where(x => !string.IsNullOrEmpty(x.Example) || !string.IsNullOrEmpty(x.Definition))
            //    .ToList();

            //var definitionsFromAdjective = translationResultRaw[0].Meaning.Adjective
            //    .Select(x => new TranslationDefinition() { Definition = x.Definition, Example = x.Example })
            //    .Where(x => !string.IsNullOrEmpty(x.Definition) || !string.IsNullOrEmpty(x.Example))
            //    .ToList();

            //

            //translationResult.Results = definitionsFromNoun
            //    .Select()



            //            translationResult.Results = translationResultRaw
            //.Select(x => new Result()
            //{
            //Word = x.hwi.Word,
            //ShortTranslation = x.ShortTranslation,
            //    //jak to rozwalic?
            //    Pronounciations =
            //{ new Pronounciation { Sound = x.hwi.prs
            //         .Where
            //         (x => !string.IsNullOrEmpty(x.sound.audio))
            //         .FirstOrDefault()?.sound ?? x.hwi.prs.FirstOrDefault().sound //this was prepopulated in constructor so either first not empty element or just first one (it could be either populated or not)
            //         ,Pron = x.hwi.prs
            //         .Where (x => !string.IsNullOrEmpty(x.ipa)).FirstOrDefault()?.ipa
            // } }
            //})
            ////.Where(x => !string.IsNullOrEmpty(x.Example) || !string.IsNullOrEmpty(x.Definition))
            //.ToList();


            //translationResult.

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

        public Task ListenAudio(string phrase)
        {
            throw new NotImplementedException();
        }
    }
}

