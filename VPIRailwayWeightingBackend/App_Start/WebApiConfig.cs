using System.Web.Http;

namespace RecycleBitBackEnd {

    /// <summary>
    /// Web API configuration
    /// </summary>
    public static class WebApiConfig {

        /// <summary>
        /// Register method
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config) {
            // Serviços e configuração da API da Web

            config.EnableCors();
            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}