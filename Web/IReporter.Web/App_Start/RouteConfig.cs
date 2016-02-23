namespace IReporter.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Categories",
                url: "{action}",
                defaults: new { controller = "ByCategory" });
            routes.MapRoute(
                name: "Articles",
                url: "articles/{route}",
                defaults: new { controller = "ArticleDetails", action = "Index", route = UrlParameter.Optional });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
