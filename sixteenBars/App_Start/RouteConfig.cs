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
                name: "Artist",
                url: "Artists/{artistname}",
                defaults: new { controller = "Artist", action = "NameDetails" }
            );

            routes.MapRoute(
                name: "Album",
                url: "Albums/{artistname}/{albumtitle}",
                defaults: new { controller = "Album", action = "NameTitleDetails" }
            );

            routes.MapRoute(
                name: "Track",
                url: "Tracks/{albumtitle}/{tracktitle}",
                defaults: new { controller = "Track", action = "TitleTitleDetails" }
            );

            routes.MapRoute(
                name: "Quote",
                url: "Quotes/{speakername}/{quotetext}",
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