using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Util;
using System;

namespace RecycleBitBackEnd {

    public partial class Startup {

        public void ConfigureVariables() {
            ApplicationConfig.APP_MODE = ApplicationParameters.Params.Environment;
            try {
                ApplicationConfig.APPLICATION_NAME = ApplicationParameters.Params.ApplicationName;
                ApplicationConfig.SQL_DB_NAME = ApplicationParameters.Params.Properties.SQLDBName;
                ApplicationConfig.SQL_INSTANCE_NAME = ApplicationParameters.Params.Properties.SQLInstanceName;
                //ApplicationConfig.SQL_USER = Environment.GetEnvironmentVariable((string)ApplicationParameters.Params.Properties.EnvSQLUser);
                //ApplicationConfig.SQL_USER_PASSWORD = Environment.GetEnvironmentVariable((string)ApplicationParameters.Params.Properties.EnvSQLUserPassword);
                ApplicationConfig.SQL_USER = ApplicationParameters.Params.Properties.EnvSQLUser;
                ApplicationConfig.SQL_USER_PASSWORD = ApplicationParameters.Params.Properties.EnvSQLUserPassword;

                ApplicationConfig.START_JOBS = ApplicationParameters.Params.Properties.StartJob;
            } catch (Exception ex) {
                throw new Exception($"Erro ao ler as variáveis de ambiente. {ex.Message}", ex);
            }

            try {
                ApplicationConfig.FileNLogPath = ApplicationParameters.Params.Properties.LogFilePath;
                ApplicationConfig.NLogMaxArchiveDays = ApplicationParameters.Params.Properties.NLogMaxArchiveDays;
                ApplicationConfig.NLogMinLevel = (string)ApplicationParameters.Params.Properties.NLogMinLevel;
                ApplicationConfig.NLogMaxLevel = (string)ApplicationParameters.Params.Properties.NLogMaxLevel;
            } catch {
                ApplicationConfig.FileNLogPath = "c:/Logs/Pesagem/";
                ApplicationConfig.NLogMaxArchiveDays = 10;
                ApplicationConfig.NLogMinLevel = "Trace";
                ApplicationConfig.NLogMaxLevel = "Fatal";
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