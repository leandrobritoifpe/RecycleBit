using Autofac;
using HarpiaCommon.Config;
using HarpiaCommon.Models.Request;
using HarpiaCommon.Services.Interfaces;
using Newtonsoft.Json;
using Prometheus;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Util;
using RecycleBitBackEnd.Util.Prometheus;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RecycleBitBackEnd {

    /// <summary>
    ///    Global iniatialize application.
    /// </summary>
    public class WebApiApplication : HttpApplication {
        private static readonly IHttpModule Module = new PrometheusHttpRequestModule();

        /// <summary>
        ///     Method responsible for initializing the application
        /// </summary>
        protected void Application_Start() {
            string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "/ApplicationInitConfiguration/ApplicationConfig.json";
            ApplicationParameters.Params = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(jsonPath));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            if ((string)ApplicationParameters.Params.Properties.EnableMetrics == "TRUE") {
                AspNetMetricServer.RegisterRoutes(GlobalConfiguration.Configuration);//Prometheus
            }
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Method to keep the application alive
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        protected void Application_End(object sender, EventArgs e) {
            IHarpiaLoggerBO loggerBO = ApplicationConfig.Container.Resolve<IHarpiaLoggerBO>();
            loggerBO.SetDebugLog(new NewLogRequest(CommonConfig.ApplicationName, DateTime.Now, $"Aplica��o finalizou", string.Empty, string.Empty));
        }

        /// <summary>
        /// Initialization method
        /// </summary>
        public override void Init() {
            base.Init();
            Module.Init(this);
        }
    }
}