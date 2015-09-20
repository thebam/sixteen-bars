using System.Web.Mvc;
using System.Web.Routing;

namespace sixteenBars
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Quote",
                url: "{speakername}/{quotetext}",
                defaults: new { controller = "Quote", action = "NameQuoteDetails" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

         
        }
    }
}