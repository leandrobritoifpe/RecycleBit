//using HarpiaCommon.Models.Dto;
//using HarpiaCommon.Models.Request;
//using HarpiaCommon.Services.Interfaces;
//using RecycleBitBackEnd.Config;
//using RecycleBitBackEnd.Util;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;

//namespace RecycleBitBackEnd.Controllers {

//    /// <summary>
//    ///     Class of contorller iinformation references schedulle job
//    /// </summary>
//    public class JobScheduleInfoController : Controller {
//        private readonly IHarpiaLoggerBO loggerBO;
//        private readonly ISchedulerBO schedulerBO;

//        public JobScheduleInfoController() {
//        }

//        public JobScheduleInfoController(IHarpiaLoggerBO loggerBO, ISchedulerBO schedulerBO) {
//            this.loggerBO = loggerBO ?? throw new ArgumentNullException("loggerBO");
//            this.schedulerBO = schedulerBO ?? throw new ArgumentNullException("schedulerBO");
//        }

//        /// <summary>
//        ///     Method responsable per process action.
//        /// </summary>
//        /// <returns>
//        ///     View API
//        /// </returns>
//        public ActionResult JobScheduleInfo() {
//            try {
//                List<JobModelDto> jobList = schedulerBO.GetCurrentJobsScheduled().Where(j => j.Running).ToList();

//                ViewBag.AllJobsInfo = jobList;
//            } catch (Exception e) {
//                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, DictionaryError.ERROR_READ_VPILOGGER, String.Empty, this.GetMethodContext(), e, e.StackTrace));
//            }
//            return View();
//        }
//    }
//}