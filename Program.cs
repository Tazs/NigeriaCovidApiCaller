using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Microsoft.AspNet.WebApi.Client;


namespace covid.apicall
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the COVID analysis machine");
            Console.WriteLine("This is a basic analysis of the COVID spread in Nigeria. Using APIs and Machine Learning");
            Console.WriteLine("Click Enter to Start");
            Console.ReadLine();

            //DeserializeTest();
            //Console.Read();
            //return;

            Info info = await GetApiValues();
            ShowInfo(info);

            GetHtmlAsync();

            Console.Read();
        }

        //private static void DeserializeTest()
        //{
        //    string jsonstring = "{\"totalSamplesTested\": \"125090\",\"totalConfirmedCases\": 22614,\"totalActiveCases\": 14243,\"discharged\": 7822,\"death\": 549,\"states\": [" +
        //    "{\"state\": \"Lagos\",\"_id\": \"S3CJLRqbW\",\"confirmedCases\": 9482,\"casesOnAdmission\": 7881,\"discharged\": 1475,\"death\": 126 }]}";
        //    List<Info> info = new List<Info>(); info.Add(JsonConvert.DeserializeObject<Info>(jsonstring));
        //}

        static void ShowInfo(Info info)
        {
            Console.WriteLine($"totalSamplesTested: {info.totalSamplesTested}" +
                $"\r\ntotalConfirmedCases: {info.totalConfirmedCases}" +
                $"\r\notalActiveCases: {info.totalActiveCases}" +
                $"\r\ndischarged: {info.discharged}" +
                $"\r\ndeath: {info.death}");

            for(int i = 0; i < info.states.Count; i++)
            {
                Console.WriteLine($"states:{info.states[i].state}" + $"\tconfirmedCases: {info.states[i].confirmedCases}" +
                $"\tcasesOnAdmission: {info.states[i].casesOnAdmission}" +
                $"\tdischarged: {info.states[i].discharged}" +
                $"\tdeath: {info.states[i].death}");
            }
        }

        private static async Task<Info> GetApiValues()
        {
            string url = "https://covidnigeria.herokuapp.com/api";

            HttpClient client = new HttpClient();

            Info info = null;
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                //info = await response.Content.ReadAsAsync<Info>();
                string jsonstring = await response.Content.ReadAsStringAsync();
                jsonstring = jsonstring.Replace("{\"data\":", "").Replace("}]}}", "}]}");
                info = JsonConvert.DeserializeObject<Info>(jsonstring);
            }
            return info;
        }

        private static void GetHtmlAsync()
        {
            var url = "https://www.worldometers.info/world-population/nigeria-population";

            var httpclient = new HttpClient();

            var html = httpclient.GetStringAsync(url);

            Console.WriteLine(html.Result);
        }
    }
}
