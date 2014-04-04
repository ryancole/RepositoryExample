using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;

namespace RepositoryExample.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // detect the current culture
            var language = Thread.CurrentThread.CurrentUICulture.Name;

            // configure default route
            routes.MapRoute("Default", "{language}/{controller}/{action}/{id}", new
            {    
                id = UrlParameter.Optional,
                action = "Index",
                controller = "Character",
                language = language
            });

        }
    }
}