using HarpiaCommon.Config;
using HarpiaCommon.Exceptions;
using HarpiaCommon.Models.Request;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Mvc;

namespace RecycleBitBackEnd.Controllers {

    /// <summary>
    ///
    /// </summary>
    public class LogsFilesController : Controller {
        private readonly IHarpiaLoggerBO loggerBO;

        public LogsFilesController() {
        }

        public LogsFilesController(IHarpiaLoggerBO loggerBO) {
            this.loggerBO = loggerBO ?? throw new ArgumentNullException("loggerBO");
        }

        /// <summary>
        ///     Method responsable per process action.
        /// </summary>
        /// <returns>
        ///     View API
        /// </returns>
        public ActionResult LogsFiles() {
            DirectoryInfo directoryInfo = new DirectoryInfo(CommonConfig.FileNLogPath);
            if (directoryInfo.Exists) {
                List<FileInfo> Files = directoryInfo.GetFiles("*.log").ToList().OrderByDescending(order => order.LastWriteTime).ToList();
                ViewBag.logList = Files;
            } else {
                ViewBag.logList = new List<FileInfo>();
            }
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        /// <summary>
        ///     Method responsable per download file zip
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public FileStreamResult DownloadFile(string fileFullName) {
            try {
                string physicalPath = Path.Combine(CommonConfig.FileNLogPath, fileFullName.EndsWith(".log") ? fileFullName : fileFullName + ".log");

                string zipPath = Path.Combine(CommonConfig.FileNLogPath, fileFullName.Replace(".log", ".zip"));

                if (System.IO.File.Exists(zipPath)) {
                    System.IO.File.Delete(zipPath);
                }

                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create)) {
                    archive.CreateEntryFromFile(physicalPath, fileFullName);
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(zipPath);
                MemoryStream ms = new MemoryStream(fileBytes);
                return new FileStreamResult(ms, "application/zip") { FileDownloadName = zipPath };
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