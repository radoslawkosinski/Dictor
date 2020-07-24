using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Smieci
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //Console.WriteLine("Hello World!");
            //string req = $"https://www.dictionaryapi.com/api/v3/references/learners/json/apple?key=4d00665c-951a-47e1-a2b7-fefbd95daf6a";
            await spr();
            //try
            //{
            //    var httpClient = new HttpClient();

            //    var response = await httpClient.GetAsync(req);
            //    if (response.StatusCode != HttpStatusCode.OK)
            //        // return null;
            //        Console.WriteLine("bad");
            //    var stream = await response.Content.ReadAsStreamAsync();
            //    //return stream;
            //    Console.WriteLine("ok?");
            //}
            //catch (Exception ex)
            //{
            //    //return null;
            //    Console.WriteLine(ex.Message);
            //}

            //Console.WriteLine("blabla");
        }

        static async Task spr() {
            string req = $"https://www.dictionaryapi.com/api/v3/references/learners/json/apple?key=4d00665c-951a-47e1-a2b7-fefbd95daf6a";

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync(req);
                if (response.StatusCode != HttpStatusCode.OK)
                    // return null;
                    Console.WriteLine("bad");
                var stream = await response.Content.ReadAsStreamAsync();
                //return stream;
                Console.WriteLine("ok?");
            }
            catch (Exception ex)
            {
                //return null;
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("blabla");
        }




    }
}
