using RecycleBitBackEnd.Services.Interfaces;
using System;
using System.Web.Mvc;

namespace RecycleBitBackEnd.Controllers {

    /// <summary>
    ///     Home controller config default page.
    /// </summary>
    public class HomeController : Controller {
        private IDiagnosticsBO diagnosticsBO;

        public HomeController() {
        }

        public HomeController(IDiagnosticsBO diagnosticsBO) {
            this.diagnosticsBO = diagnosticsBO ?? throw new ArgumentNullException("diagnosticsBO");
        }

        /// <summary>
        ///     Method responsable per process action.
        /// </summary>
        /// <returns>
        ///     View API
        /// </returns>
        public ActionResult Index() {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            string displayableVersion = $"{version} ({buildDate})";
            ViewBag.Title = "RecycleBitBackEnd - Home";
            ViewBag.AvaliableVersion = displayableVersion;
            ViewBag.Diagnostics = diagnosticsBO.GetDiagnosticsFromSolution();
            return View();
        }
    }
}