using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace site_real
{
    public class CountryInfoHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token)
        {
            HttpContent content;
            HttpResponseMessage response = new HttpResponseMessage();
            Console.WriteLine(message.RequestUri.LocalPath);

            var parsedUri = message.RequestUri.LocalPath.Split('/');
            if (message.Method.Method == "GET")
            {
                switch (parsedUri[2])
                {
                    case "get":
                        CountryInfo info = null;
                        using (DBHandler db = new DBHandler())
                        {
                            info = db.Countries[parsedUri[3]];
                        }
                        content = new StringContent(JsonConvert.SerializeObject(info));
                        content.Headers.ContentType.MediaType = "application/json";
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = content
                        };
                        break;
                    case "getall":
                        using (DBHandler db = new DBHandler())
                        {
                            string[] names = db.GetAllCountryNames();
                            content = new StringContent(JsonConvert.SerializeObject(names));
                            content.Headers.ContentType.MediaType = "application/json";
                            response = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = content
                            };
                        }
                        break;
                    case "getcodes":
                        using (DBHandler db = new DBHandler())
                        {
                            string[] names = db.GetAllCountryCodes();
                            content = new StringContent(JsonConvert.SerializeObject(names));
                            content.Headers.ContentType.MediaType = "application/json";
                            response = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = content
                            };
                        }
                        break;
                }
            }
            else if (message.Method.Method == "POST")
            {
                string body = await message.Content.ReadAsStringAsync();
                CountryInfo info = JsonConvert.DeserializeObject<CountryInfo>(body);
                if (parsedUri[2] != "setadmin") info.OfficialArticle = null;
                using (DBHandler db = new DBHandler())
                {
                    db.Countries[parsedUri[3]] = info;
                }
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }
    }
}