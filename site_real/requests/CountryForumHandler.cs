using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using System.Net;

namespace site_real
{
    public class CountryForumHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token)
        {
            HttpContent Content;
            HttpResponseMessage response = new HttpResponseMessage();
            string[]ParsedUri= message.RequestUri.LocalPath.Split('/');
            int id = int.Parse( ParsedUri[2]);
            if (message.Method.Method == "GET")
            {
                DBHandler.Open();
                Comment[] comments = DBHandler.GetCountryComments(id);
                Content = new StringContent(JsonConvert.SerializeObject(comments));
                Content.Headers.ContentType.MediaType = "application/json"; 
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = Content
                };
            }
            else
            {
                string Body = await message.Content.ReadAsStringAsync();
                Comment comment = JsonConvert.DeserializeObject<Comment>(Body);
                DBHandler.Open();
                DBHandler.AddNewComment(comment);
                DBHandler.Close();
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }
    }
}