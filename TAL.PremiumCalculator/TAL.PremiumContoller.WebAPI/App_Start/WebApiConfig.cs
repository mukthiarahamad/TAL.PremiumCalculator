﻿using System.Web.Http;
using System.Web.Http.Cors;

namespace TAL.PremiumCalculator.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");//origins,headers,methods   
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

             config.Routes.MapHttpRoute(
             name: "Root",
             routeTemplate: "",
             defaults: new { controller = "Premium", action = "Index" }
 );
        }
    }
}
