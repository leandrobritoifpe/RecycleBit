using System.Web.Mvc;
using System.Web.Routing;

namespace RecycleBitBackEnd {

    /// <summary>
    /// Route configuration class
    /// </summary>
    public static class RouteConfig {

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes collection</param>
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}