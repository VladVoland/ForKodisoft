using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL;


namespace ForKodisoft
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            BLLAutoMapper.Initialize();
            // Web API configuration and services

            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
