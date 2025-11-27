using HarpiaCommon.Models.Dto;
using HarpiaCommon.Models.Request;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecycleBitBackEnd.Controllers {

    public class JobExecutionController : Controller {
        private readonly IHarpiaLoggerBO loggerBO;
        private readonly ISchedulerBO schedulerBO;

        public JobExecutionController() {
        }

        public JobExecutionController(IHarpiaLoggerBO loggerBO, ISchedulerBO schedulerBO) {
            this.loggerBO = loggerBO ?? throw new ArgumentNullException("loggerBO");
            this.schedulerBO = schedulerBO ?? throw new ArgumentNullException("schedulerBO");
        }

        /// <summary>
        ///     Method responsable per process action.
        /// </summary>
        /// <returns>
        ///     View API
        /// </returns>
        public ActionResult JobExecution() {
            List<DPABT_JOB_EXECUTION> logsJobExecution = loggerBO.FindJogExecutionLogsFromParameters(new JobExecutionsByParametersRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now.AddDays(-1 * ApplicationConfig.DAYS_TO_SEARCH_LOGS), DateTime.Now, string.Empty, string.Empty));
            ViewBag.Last24HoursLogsJobList = logsJobExecution;

            JobModelDto job = schedulerBO.GetCurrentJobsScheduled().Where(j => j.Running).FirstOrDefault();
            if (job != null) {
                ViewBag.NextJobExecution = job.NextRunDateTimeOffset.HasValue ? job.NextRunDateTimeOffset.Value.ToString("dd-MM-yyyy HH:mm:ss") : "";
                ViewBag.JobsRunning = job.Running ? "Running" : "Stopped";
            }

            return View();
        }
    }
}