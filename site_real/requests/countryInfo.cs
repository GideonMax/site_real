using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web.Http;
using site_real.App_Code;
using Newtonsoft.Json;

namespace site_real
{
    public class CountryInfoHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Console.WriteLine(message.RequestUri.LocalPath);
            
            var parsedUri = message.RequestUri.LocalPath.Split('/');
            switch (parsedUri[2])
            {
                case "get":
                    string article = null;
                    using (DBHandler db = new DBHandler())
                    {
                        article = db.GetArticleByCountryCode(parsedUri[parsedUri.Length - 1]);
                    }
                    if (article != null)
                    {
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(article)
                        };
                    }
                    else
                    {
                        response = new HttpResponseMessage(HttpStatusCode.SeeOther)
                        {
                            Content = null
                        };
                    }
                    break;
                case "getall":
                    using( DBHandler db = new DBHandler())
                    {
                        string[] names = db.GetAllCountryNames();
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(names));
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
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(names));
                        content.Headers.ContentType.MediaType = "application/json";
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = content
                        };
                    }
                    break;
            }
            TaskCompletionSource<HttpResponseMessage> task = new TaskCompletionSource<HttpResponseMessage>();
            task.SetResult(response);
            return task.Task;
        }
    }
    /*public class CountryInfo : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "no country code given";
        }

        // GET api/<controller>/5
        [HttpGet]
        string countryinfo(string id)
        {
            using(DBHandler db = new DBHandler())
            {
                return db.GetArticleByCountryCode(id);
            }
        }
        
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        
    }*/
}