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

namespace site_real
{
    public class CountryInfo : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token)
        {
            Console.WriteLine("yes");
            string article = null;
            using(DBHandler db = new DBHandler())
            {
                var parsedUri = message.RequestUri.OriginalString.Split('/');
                article = db.GetArticleByCountryCode(parsedUri[parsedUri.Length-1] );
            }
            HttpResponseMessage response;
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