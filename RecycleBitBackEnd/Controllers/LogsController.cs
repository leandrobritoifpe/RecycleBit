//using HarpiaCommon.Models.Dto;
//using HarpiaCommon.Models.Request;
//using HarpiaCommon.Services.Interfaces;
//using RecycleBitBackEnd.Config;
//using RecycleBitBackEnd.Util;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Web.Mvc;

//namespace RecycleBitBackEnd.Controllers {

//    /// <summary>
//    /// Controller of Logs Page
//    /// </summary>
//    public class LogsController : Controller {
//        private readonly IHarpiaLoggerBO loggerBO;

//        public LogsController() {
//        }

//        public LogsController(IHarpiaLoggerBO loggerBO) {
//            this.loggerBO = loggerBO ?? throw new ArgumentNullException("loggerBO");
//        }

//        /// <summary>
//        ///     Method responsable per process action.
//        /// </summary>
//        /// <returns>
//        ///     View API
//        /// </returns>
//        public ActionResult Logs() {
//            try {
//                List<DPAAT_LOG_ERROR> logErrorEntries = loggerBO.FindErrorLogsFromParameters(new LogsByParametersRequest((string)ApplicationParameters.Params.ApplicationName, string.Empty, DateTime.Now.AddDays(-1 * ApplicationConfig.DAYS_TO_SEARCH_LOGS).ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture)));
//                ViewBag.Last24HoursLogsList = logErrorEntries;
//            } catch (Exception e) {
//                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, DictionaryError.ERROR_READ_VPILOGGER, String.Empty, this.GetMethodContext(), e, e.StackTrace));
//            }
//            return View();
//        }
//    }
//}