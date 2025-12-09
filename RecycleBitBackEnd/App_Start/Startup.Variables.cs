using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Util;
using System;

namespace RecycleBitBackEnd {

    public partial class Startup {

        public void ConfigureVariables() {
            ApplicationConfig.APP_MODE = ApplicationParameters.Params.Environment;
            try {
                ApplicationConfig.APPLICATION_NAME = ApplicationParameters.Params.ApplicationName;
                ApplicationConfig.SQL_USER = Environment.GetEnvironmentVariable((string)ApplicationParameters.Params.Properties.RecycleSQLUser, EnvironmentVariableTarget.Machine);
                ApplicationConfig.SQL_USER_PASSWORD = Encriptor.simpleDecriptBase64(Environment.GetEnvironmentVariable((string)ApplicationParameters.Params.Properties.RecycleSQLUserPassword, EnvironmentVariableTarget.Machine));
                ApplicationConfig.START_JOBS = ApplicationParameters.Params.Properties.StartJob;
                ApplicationConfig.SQL_INSTANCE_NAME = ApplicationParameters.Params.Properties.SQLInstanceName;
                ApplicationConfig.SQL_DB_NAME = ApplicationParameters.Params.Properties.SQLDBName;
            } catch (Exception ex) {
                throw new Exception($"Erro ao ler as variáveis de ambiente. {ex.Message}", ex);
            }

            try {
                ApplicationConfig.FileNLogPath = ApplicationParameters.Params.Properties.LogFilePath;
                ApplicationConfig.NLogMaxArchiveDays = ApplicationParameters.Params.Properties.NLogMaxArchiveDays;
                ApplicationConfig.LogMinLevel = (string)ApplicationParameters.Params.Properties.NLogMinLevel;
                ApplicationConfig.LogMaxLevel = (string)ApplicationParameters.Params.Properties.NLogMaxLevel;
            } catch {
                ApplicationConfig.FileNLogPath = "c:/Logs/Pesagem/";
                ApplicationConfig.NLogMaxArchiveDays = 10;
                ApplicationConfig.LogMinLevel = "Trace";
                ApplicationConfig.LogMaxLevel = "Fatal";
            }

            //ApplicationConfig.LastDateTime = ApplicationParameters.Params.Properties.LastDatetime;

            //try {
            //    ApplicationConfig.HoursToSearchLog = ApplicationParameters.Params.Properties.HoursToSearchLog;
            //} catch {
            //    ApplicationConfig.HoursToSearchLog = 12;
            //}
        }
    }
}