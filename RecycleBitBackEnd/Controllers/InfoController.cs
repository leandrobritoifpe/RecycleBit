using HarpiaCommon.Models.Request;
using HarpiaCommon.Services.Interfaces;
using Newtonsoft.Json.Linq;
using RecycleBitBackEnd.Util;
using RecycleBitBackEnd.Util.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RecycleBitBackEnd.Controllers {

    /// <summary>
    /// Controller to configure the solution APIs related to info of the application.
    /// </summary>
    [RoutePrefix("api/info")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InfoController : ApiController {
        private readonly IHarpiaLoggerBO loggerBO;

        public InfoController() {
        }

        public InfoController(IHarpiaLoggerBO loggerBO) {
            this.loggerBO = loggerBO ?? throw new ArgumentNullException("loggerBO");
        }

        /// <summary>
        /// Get the current version of the application.
        /// </summary>
        /// <returns>Current version of the application</returns>
        [AcceptVerbs("Get")]
        [ActionName("GetInfoAPI")]
        public string GetInfoAPI() {
            return (string)ApplicationParameters.Params.Version;
        }

        /// <summary>
        /// Get the current version of the application.
        /// </summary>
        /// <returns>Current version of the application</returns>
        [AcceptVerbs("Get")]
        [ActionName("GetEnvironmentVariable")]
        public string GetEnvironmentVariable(string variable, string type) {
            return type switch {
                "Machine" => Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Machine),
                "User" => Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.User),
                "Process" => Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Process),
                _ => Environment.GetEnvironmentVariable(variable),
            };
        }

        [AcceptVerbs("Get")]
        [ActionName("ListAllEnv")]
        public IDictionary<string, string> ListAllEnv(EnvironmentVariableTarget variableTarget) {
            var dict = new Dictionary<string, string>();

            foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables(variableTarget)) {
                dict[de.Key.ToString()] = de.Value?.ToString();
            }
            return dict;
        }

        [AcceptVerbs("Get")]
        [ActionName("GetPowerShellCommandValue")]
        public string GetPowerShellCommandValue(string key) {
            using Process process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    Arguments = $"/c echo %{key}%",
                    CreateNoWindow = true,
                    WorkingDirectory = string.Empty,
                }
            };
            process.Start();
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd().Trim();
        }

        [AcceptVerbs("Get")]
        [ActionName("GetApplicationParametersValue")]
        public string GetApplicationParametersValue(string path) {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            JToken current = ApplicationParameters.Params as JToken;
            foreach (var part in path.Split('.')) {
                if (current == null)
                    return null;

                if (current.Type == JTokenType.Object || current.Type == JTokenType.Array) {
                    current = current[part];
                } else {
                    return null;
                }
            }

            if (current == null)
                return null;

            if (current.Type == JTokenType.Object || current.Type == JTokenType.Array)
                return current.ToString(Newtonsoft.Json.Formatting.None);

            return current.ToString();
        }

        /// <summary>
        /// Get the current version of the application.
        /// </summary>
        /// <returns>Current version of the application</returns>
        [AcceptVerbs("Get")]
        [ActionName("LogTestInDatabase")]
        public string LogTestInDatabase() {
            loggerBO.SetDebugLog(new NewLogRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, "Message", "Site", "AdditionalInfo"));
            try {
                throw new Exception("ErrorMessage");
            } catch (Exception e) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, e.Message, "Site", "AdditionalInfo", e, e.StackTrace));
            }
            return StatusEnum.OK;
        }
    }
}