using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace site_real
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "countryApi1",
                routeTemplate: "country/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints:null,
                handler: new CountryInfo()
            );
            config.Routes.MapHttpRoute(
                name: "countryApi2",
                routeTemplate: "country/{id}/{yes}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: new CountryInfo()
            );
        }
    }
}
