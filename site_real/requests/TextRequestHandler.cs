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

namespace site_real.requests
{
    /*public class TextRequestHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Console.WriteLine(message.RequestUri.LocalPath);

            var parsedUri = message.RequestUri.LocalPath.Split('/');
        }
    }*/
}