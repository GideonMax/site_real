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
            var parsedUri = message.RequestUri.LocalPath.Split('/');
            string TextName = parsedUri[2];
            if (message.Method.Method == "GET")
            {
                //using (OutPutFile f = new OutPutFile()) f.WriteLine("text get");
                DBHandler.Open();
                    string text = DBHandler.Texts[TextName];
                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(text)
                    };
                DBHandler.Close();
            }
            else if (message.Method.Method == "POST")
            {
                //using (OutPutFile f = new OutPutFile()) f.WriteLine("text post");
                string text = await message.Content.ReadAsStringAsync();
                DBHandler.Open();
                DBHandler.Texts[TextName] = text;
                DBHandler.Close();
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }
    }
}