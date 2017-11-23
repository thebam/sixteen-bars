using System.Web.Http;
namespace sixteenBars
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ArtistApi",
                routeTemplate: "api/Artists/{action}",
                defaults: new { controller="ArtistAPI", name = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "AlbumApi",
                routeTemplate: "api/Albums/{action}",
                defaults: new { controller = "AlbumAPI", name = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "TrackApi",
                routeTemplate: "api/Tracks/{action}",
                defaults: new { controller = "TrackAPI", name = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "QuoteApi",
                routeTemplate: "api/Quotes/{action}",
                defaults: new { controller = "QuoteAPI", name = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name:"CustomApi",
                routeTemplate:"api/{controller}/{action}/{name}",
                defaults: new { name = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
            //config.DependencyResolver = new ResolverController();
        }
    }
}