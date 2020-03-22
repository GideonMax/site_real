using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web.Http;
using Newtonsoft.Json;

namespace site_real
{

    public class TextRequestHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Console.WriteLine(message.RequestUri.LocalPath);

            var parsedUri = message.RequestUri.LocalPath.Split('/');
            string TextName = parsedUri[2];
            if (message.Method.Method == "GET")
            {
                using (DBHandler db = new DBHandler())
                {
                    string text = db.Texts[TextName];
                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(text)
                    };
                }
            }
            else if (message.Method.Method == "POST")
            {
                string text = await message.Content.ReadAsStringAsync();
                using (DBHandler db = new DBHandler())
                {
                    db.Texts[TextName] = text;
                }
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }
    }
}