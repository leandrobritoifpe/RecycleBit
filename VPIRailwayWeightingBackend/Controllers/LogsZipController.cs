using HarpiaCommon.Config;
using HarpiaCommon.Exceptions;
using HarpiaCommon.Models.Request;
using HarpiaCommon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RecycleBitBackEnd.Util;

namespace RecycleBitBackEnd.Controllers {

    public class LogsZipController : Controller {
        private readonly IHarpiaLoggerBO loggerBO;

        public LogsZipController() {
        }

        public LogsZipController(IHarpiaLoggerBO loggerBO) {
            this.loggerBO = loggerBO ?? throw new ArgumentNullException("loggerBO");
        }

        /// <summary>
        ///     Method responsable per process action.
        /// </summary>
        /// <returns>
        ///     View API
        /// </returns>
        public ActionResult LogsZip() {
            try {
                DirectoryInfo directoryInfo = new DirectoryInfo(CommonConfig.FileNLogPath);
                if (directoryInfo.Exists) {
                    List<FileInfo> Files = directoryInfo.GetFiles("*.zip").ToList().OrderByDescending(order => order.LastWriteTime).ToList();
                    ViewBag.logList = Files;
                } else {
                    ViewBag.logList = new List<FileInfo>();
                }
            } catch (UtilException exception) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, exception.Message, string.Empty, this.GetMethodContext(), exception, exception.StackTrace));
            } catch (ProjectException exception) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, exception.Message, string.Empty, this.GetMethodContext(), exception, exception.StackTrace));
            } catch (Exception exception) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, exception.Message, string.Empty, this.GetMethodContext(), exception, exception.StackTrace));
            }
            return View();
        }

        public FileStreamResult DownloadFile(string fileFullName) {
            try {
                string physicalPath = Path.Combine(CommonConfig.FileNLogPath, fileFullName.EndsWith(".zip") ? fileFullName : fileFullName + ".zip");
                string physicalPathCopy = Path.Combine(CommonConfig.FileNLogPath, fileFullName.EndsWith(".zip") ? fileFullName.Replace(".zip", DateTime.Now.Millisecond.ToString() + ".zip") : fileFullName + DateTime.Now.Millisecond.ToString() + ".zip");
                System.IO.File.Copy(physicalPath, physicalPathCopy);
                byte[] fileBytes = System.IO.File.ReadAllBytes(physicalPathCopy);
                MemoryStream ms = new MemoryStream(fileBytes);
                System.IO.File.Delete(physicalPathCopy);
                return new FileStreamResult(ms, "application/zip") { FileDownloadName = fileFullName };
            } catch (UtilException exception) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, exception.Message, string.Empty, this.GetMethodContext(), exception, exception.StackTrace));
            } catch (ProjectException exception) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, exception.Message, string.Empty, this.GetMethodContext(), exception, exception.StackTrace));
            } catch (Exception exception) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, exception.Message, string.Empty, this.GetMethodContext(), exception, exception.StackTrace));
            }
            return null;
        }
    }
}