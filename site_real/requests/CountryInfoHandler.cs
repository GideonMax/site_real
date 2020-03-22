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
                        using (OutPutFile f = new OutPutFile()) f.WriteLine("country get");
                        CountryInfo info = null;
                        using (DBHandler db = new DBHandler())
                        {
                            if (db.DoesCountryExist(parsedUri[3]))
                            {
                                info = db.Countries[parsedUri[3]];
                            }
                            else
                            {
                                info = null;
                            }
                        }
                        using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(message.RequestUri.LocalPath));
                        using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(info,Formatting.Indented));
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
                            using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(names));
                            content = new StringContent(JsonConvert.SerializeObject(names));
                            content.Headers.ContentType.MediaType = "application/json";
                            response = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = content
                            };
                        }
                        break;
                    case "getcodes":
                        using (DBHandler db1 = new DBHandler())
                        {
                            string[] names = db1.GetAllCountryCodes();
                            using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(names));
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