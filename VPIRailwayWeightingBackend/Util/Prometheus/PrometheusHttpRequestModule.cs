using Prometheus;
using System;
using System.Diagnostics;
using System.Web;

namespace RecycleBitBackEnd.Util.Prometheus {

    /// <summary>
    /// Prometheus http interface for requests
    /// </summary>
    public class PrometheusHttpRequestModule : IHttpModule {

        private static readonly Counter _globalExceptions = Metrics
          .CreateCounter("global_exceptions", "Number of global exceptions.");

        private static readonly Gauge _httpRequestsInProgress = Metrics
            .CreateGauge("http_requests_in_progress", "The number of HTTP requests currently in progress");

        private static readonly Gauge _httpRequestsTotal = Metrics
            .CreateGauge("http_requests_received_total", "Provides the count of HTTP requests that have been processed by this app",
                new GaugeConfiguration { LabelNames = new[] { "code", "method", "controller", "action" } });

        private static readonly Histogram _httpRequestsDuration = Metrics
            .CreateHistogram("http_request_duration_seconds", "The duration of HTTP requests processed by this app.",
                new HistogramConfiguration { LabelNames = new[] { "code", "method", "controller", "action" } });

        private const string _timerKey = "PrometheusHttpRequestModule.Timer";

        /// <summary>
        /// Initializer
        /// </summary>
        /// <param name="context">Http application</param>
        public void Init(HttpApplication context) {
            context.BeginRequest += OnBeginRequest;
            context.EndRequest += OnEndRequest;
            context.Error += HttpApp_Error;
        }

        private void HttpApp_Error(object sender, EventArgs e) {
            _globalExceptions.Inc();
        }

        /// <summary>
        /// Capture info on begin request
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        // Record the time of the begin request event.
        public void OnBeginRequest(Object sender, EventArgs e) {
            _httpRequestsInProgress.Inc();

            var httpApp = (HttpApplication)sender;
            var timer = new Stopwatch();
            httpApp.Context.Items[_timerKey] = timer;
            timer.Start();
        }

        /// <summary>
        /// Capture info on end request
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        public void OnEndRequest(Object sender, EventArgs e) {
            _httpRequestsInProgress.Dec();

            var httpApp = (HttpApplication)sender;
            var timer = (Stopwatch)httpApp.Context.Items[_timerKey];

            if (timer != null) {
                timer.Stop();

                string code = httpApp.Response.StatusCode.ToString();
                string method = httpApp.Request.HttpMethod;
                var routeData = httpApp.Request.RequestContext?.RouteData?.Values;

                string controller = String.Empty;
                string action = String.Empty;

                if (routeData != null) {
                    if (routeData.ContainsKey("controller"))
                        controller = routeData["controller"].ToString();
                    if (routeData.ContainsKey("action"))
                        action = routeData["action"].ToString();
                }

                double timeTakenSecs = timer.ElapsedMilliseconds / 1000d;

                _httpRequestsDuration.WithLabels(code, method, controller, action).Observe(timeTakenSecs);
                _httpRequestsTotal.WithLabels(code, method, controller, action).Inc();
            }

            httpApp.Context.Items.Remove(_timerKey);
        }

        /// <summary>
        /// Method intentionally left empty.
        /// </summary>
        public void Dispose() {
            // Method intentionally left empty.
        }
    }
}