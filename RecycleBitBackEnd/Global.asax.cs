using Autofac;
using HarpiaCommon.Config;
using HarpiaCommon.Models.Request;
using HarpiaCommon.Services.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;
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
    ///    Global initialize application.
    /// </summary>
    public class WebApiApplication : HttpApplication {

        private static readonly ILog Logger = LogManager.GetLogger(typeof(WebApiApplication));
        private static readonly IHttpModule Module = new PrometheusHttpRequestModule();

        /// <summary>
        ///     Method responsible for initializing the application
        /// </summary>
        protected void Application_Start() {
            try {
                string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "/ApplicationInitConfiguration/ApplicationConfig.json";
                ApplicationParameters.Params = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(jsonPath));

                ConfigureLog4Net();
                Logger.Info("Application is starting...");

                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                if ((string)ApplicationParameters.Params.Properties.EnableMetrics == "TRUE") {
                    AspNetMetricServer.RegisterRoutes(GlobalConfiguration.Configuration);//Prometheus
                }
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                Logger.Info("Application started successfully.");
            } catch (Exception ex) {
                Logger.Error("An error occurred during application startup.", ex);
                throw;
            }
        }

        /// <summary>
        /// Method to keep the application alive
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        protected void Application_End(object sender, EventArgs e) {
            IHarpiaLoggerBO loggerBO = ApplicationConfig.Container.Resolve<IHarpiaLoggerBO>();
            loggerBO.SetDebugLog(new NewLogRequest(CommonConfig.ApplicationName, DateTime.Now, $"Application finish", string.Empty, string.Empty));
        }

        /// <summary>
        /// Initialization method
        /// </summary>
        public override void Init() {
            base.Init();
            Module.Init(this);
        }

        private void ConfigureLog4Net() {
            string logFilePath = ApplicationParameters.Params.Properties.LogFilePath;
            if (logFilePath == string.Empty) {
                logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"application_event-processor.log");
            }

            RollingFileAppender rollingFileAppender = new() {
                File = logFilePath,
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                MaxSizeRollBackups = 1,
                MaximumFileSize = "300MB",
                StaticLogFileName = true,
                DatePattern = "yyyyMMdd'.log'",
                Layout = new PatternLayout("%date [%thread] %-5level %logger - %message%newline"),
                LockingModel = new FileAppender.MinimalLock()
            };

            LevelRangeFilter levelFilter = new() {
                LevelMin = GetLevel((string)ApplicationParameters.Params.Properties.LogMinLevel),
                LevelMax = GetLevel((string)ApplicationParameters.Params.Properties.LogMinLevel),
                AcceptOnMatch = true
            };

            rollingFileAppender.AddFilter(levelFilter);
            rollingFileAppender.ActivateOptions();

            ILoggerRepository logRepository = LogManager.GetRepository();
            log4net.Config.BasicConfigurator.Configure(logRepository, rollingFileAppender);
        }

        private log4net.Core.Level GetLevel(string name) {
            return name switch {
                "DEBUG" => log4net.Core.Level.Debug,
                "INFO" => log4net.Core.Level.Info,
                "WARN" => log4net.Core.Level.Warn,
                "ERROR" => log4net.Core.Level.Error,
                "FATAL" => log4net.Core.Level.Fatal,
                _ => log4net.Core.Level.Debug
            };
        }
    }
}