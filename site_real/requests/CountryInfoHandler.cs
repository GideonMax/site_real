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
                        //using (OutPutFile f = new OutPutFile()) f.WriteLine("country get");
                        CountryInfo info = null;
                        DBHandler.Open();
                            if (DBHandler.DoesCountryExist(parsedUri[3]))
                            {
                                info = DBHandler.Countries[parsedUri[3]];
                            }
                            else
                            {
                                info = null;
                            }
                        DBHandler.Close();
                        //using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(message.RequestUri.LocalPath));
                        //using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(info,Formatting.Indented));
                        content = new StringContent(JsonConvert.SerializeObject(info));
                        content.Headers.ContentType.MediaType = "application/json";
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = content
                        };
                        break;
                    case "getall":
                        DBHandler.Open();
                            string[] names = DBHandler.GetAllCountryNames();
                            //using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(names));
                            content = new StringContent(JsonConvert.SerializeObject(names));
                            content.Headers.ContentType.MediaType = "application/json";
                            response = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = content
                            };
                        DBHandler.Close();
                        break;
                    case "getcodes":
                        DBHandler.Open();
                            string[] codes = DBHandler.GetAllCountryCodes();
                            //using (OutPutFile f = new OutPutFile()) f.WriteLine(JsonConvert.SerializeObject(codes));
                            content = new StringContent(JsonConvert.SerializeObject(codes));
                            content.Headers.ContentType.MediaType = "application/json";
                            response = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = content
                            };
                        DBHandler.Close();
                        break;
                }
            }
            else if (message.Method.Method == "POST")
            {
                string body = await message.Content.ReadAsStringAsync();
                CountryInfo info = JsonConvert.DeserializeObject<CountryInfo>(body);
                if (parsedUri[2] != "setadmin") {
                    info.OfficialArticle = null;
                    info.IsAdminRequest = false;
                }
                else info.IsAdminRequest = true;
                DBHandler.Open();
                    DBHandler.Countries[parsedUri[3]] = info;
                DBHandler.Close();
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }
    }
}