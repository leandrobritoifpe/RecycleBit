namespace RecycleBitBackEnd {

    /// <summary>
    ///      Get request configuration application.
    /// </summary>
    public static class AppConfigurationManager {
        ///// <summary>
        /////     Get version project.
        ///// </summary>
        ///// <returns> Version project.</returns>
        //public static string GetVersion() {
        //    return ConfigurationManager.AppSettings["version"];
        //}

        ///// <summary>
        /////     Get version API project.
        ///// </summary>
        ///// <returns> Version project.</returns>
        //public static string GetAPIVersion() {
        //    return ConfigurationManager.AppSettings["api.version"];
        //}

        ///// <summary>
        /////     Get project name.
        ///// </summary>
        ///// <returns> project name </returns>
        //public static string GetProjectName() {
        //    return ConfigurationManager.AppSettings["project.name"];
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentKeyStartJobs() {
        //    return ConfigurationManager.AppSettings["environment.key.startJobs"];
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetOPCConfigAFTablePath() {
        //    return ConfigurationManager.AppSettings["environment.key.OPCConfig.Path"];
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetWeighingConfigAFTablePath() {
        //    return ConfigurationManager.AppSettings["environment.key.WeighingConfig.Path"];
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetUsersAFTablePath() {
        //    return ConfigurationManager.AppSettings["environment.key.Users.Path"];
        //}

        ///// <summary>
        /////     Get project name.
        ///// </summary>
        ///// <returns> project name </returns>
        //public static string GetSyncTablesJobExpression() {
        //    return ConfigurationManager.AppSettings["cron.SyncTables"];
        //}

        ///// <summary>
        ///// Get IIS mode key
        ///// </summary>
        ///// <returns>IIS mode key</returns>
        //public static bool GetAppModeHasIIS() {
        //    return Convert.ToBoolean(ConfigurationManager.AppSettings["app.mode.iis"]);
        //}

        ///// <summary>
        /////     Get setting application.
        ///// </summary>
        ///// <returns> setting application. </returns>
        //public static string GetAppMode() {
        //    return ConfigurationManager.AppSettings["app.mode"];
        //}

        ///// <summary>
        /////     Command Text.
        ///// </summary>
        ///// <returns> string command text. </returns>
        //public static string GetNLogCommandText() {
        //    return ConfigurationManager.AppSettings["nlog.commandText"];
        //}

        ///// <summary>
        /////    Get NLog Minimum nivel db.
        ///// </summary>
        ///// <returns> level NLOG </returns>
        //public static string GetNLogMinLevelDB() {
        //    return ConfigurationManager.AppSettings["nlog.minLevelDB"];
        //}

        ///// <summary>
        /////    Get Maximum level nlog DB.
        ///// </summary>
        ///// <returns> string Maximum level nlog DB </returns>
        //public static string GetNLogMaxLevelDB() {
        //    return ConfigurationManager.AppSettings["nlog.maxLevelDB"];
        //}

        ///// <summary>
        /////     Get Table NLOG.
        ///// </summary>
        ///// <returns> table nlog </returns>
        //public static string GetNLogDBTable() {
        //    return ConfigurationManager.AppSettings["nlog.dbTable"];
        //}

        ///// <summary>
        /////     Get file path NLOG.
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentVariableNLogFilePath() {
        //    return ConfigurationManager.AppSettings["nlog.filePath"];
        //}

        ///// <summary>
        /////     Get file layout NLOG.
        ///// </summary>
        ///// <returns> file layout </returns>
        //public static string GetNLogFileLayout() {
        //    return ConfigurationManager.AppSettings["nlog.fileLayout"];
        //}

        ///// <summary>
        /////     Get file name NLOG.
        ///// </summary>
        ///// <returns> File name. </returns>
        //public static string GetNLogFileName() {
        //    return ConfigurationManager.AppSettings["nlog.fileName"];
        //}

        ///// <summary>
        /////     Get minimum level file.
        ///// </summary>
        ///// <returns> minimum level file </returns>
        //public static string GetNLogMinLevelFile() {
        //    return ConfigurationManager.AppSettings["nlog.minLevelFile"];
        //}

        ///// <summary>
        /////    Get maximum file level.
        ///// </summary>
        ///// <returns>  </returns>
        //public static string GetNLogMaxLevelFile() {
        //    return ConfigurationManager.AppSettings["nlog.maxLevelFile"];
        //}

        ///// <summary>
        /////     Get Provider DB
        ///// </summary>
        ///// <returns> string provider DB. </returns>
        //public static string GetNLogDBProvider() {
        //    return ConfigurationManager.AppSettings["nlog.dbProvider"];
        //}

        ///// <summary>
        /////     Enable Metrics
        ///// </summary>
        ///// <returns> password </returns>
        //public static bool EnableMetrics() {
        //    return Convert.ToBoolean(ConfigurationManager.AppSettings["enable.metrics"]);
        //}

        ////------------------------------------------------------ Azure keys config --------------------------------------------------
        ///// <summary>
        ///// Get Key Vault URL
        ///// </summary>
        ///// <returns>Key Vault URL string</returns>
        //public static string GetKeyVaultUrl() {
        //    return ConfigurationManager.AppSettings["azure.keyvault.url"];
        //}

        ///// <summary>
        ///// Get Railway Weighting Backend Identity key
        ///// </summary>
        ///// <returns>Railway Weighting Backend Identity key</returns>
        //public static string GetKeyRWBackendIdentity() {
        //    return ConfigurationManager.AppSettings["azure.key.rwbackend.iamidentity"];
        //}

        ///// <summary>
        ///// Get tenant ID
        ///// </summary>
        ///// <returns>Tenant ID</returns>
        //public static string GetTenantId() {
        //    return ConfigurationManager.AppSettings["azure.tenant"];
        //}

        ///// <summary>
        ///// Get client secret
        ///// </summary>
        ///// <returns>Client secret</returns>
        //public static string GetClientSecret() {
        //    return ConfigurationManager.AppSettings["azure.clientsecret"];
        //}

        ///// <summary>
        ///// Get client ID
        ///// </summary>
        ///// <returns>Client ID</returns>
        //public static string GetClientId() {
        //    return ConfigurationManager.AppSettings["azure.clientid"];
        //}

        ///// <summary>
        ///// Get instance database key
        ///// </summary>
        ///// <returns>Instance database key</returns>
        //public static string GetKeyInstanceDB() {
        //    return ConfigurationManager.AppSettings["azure.key.db.instance"];
        //}

        ///// <summary>
        ///// Get database name key
        ///// </summary>
        ///// <returns>Database name key</returns>
        //public static string GetKeyDBName() {
        //    return ConfigurationManager.AppSettings["azure.key.db.name"];
        //}

        ///// <summary>
        ///// Get dabase user key
        ///// </summary>
        ///// <returns>Database user key</returns>
        //public static string GetKeyDBUser() {
        //    return ConfigurationManager.AppSettings["azure.key.db.user"];
        //}

        ///// <summary>
        ///// Get dabase user's password key
        ///// </summary>
        ///// <returns>Database user's password key</returns>
        //public static string GetKeyDBPassword() {
        //    return ConfigurationManager.AppSettings["azure.key.db.password"];
        //}

        ///// <summary>
        ///// Get Simm Soft user key
        ///// </summary>
        ///// <returns>Simm Soft user key</returns>
        //public static string GetKeySimmSoftUser() {
        //    return ConfigurationManager.AppSettings["azure.key.simmsoft.user"];
        //}

        ///// <summary>
        ///// Get Simm Soft user's password key
        ///// </summary>
        ///// <returns>Simm Soft user's password key</returns>
        //public static string GetKeySimmSoftPassword() {
        //    return ConfigurationManager.AppSettings["azure.key.simmsoft.password"];
        //}

        ///// <summary>
        ///// Get Web Method value
        ///// </summary>
        ///// <returns>Web Method value</returns>
        //public static string GetKeyWebMethod() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.web.method"];
        //}

        ///// <summary>
        ///// Get Destination Host value
        ///// </summary>
        ///// <returns>Destination Host value</returns>
        //public static string GetKeyDestinationHost() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.destination.host"];
        //}

        ///// <summary>
        ///// Get Destination Port value
        ///// </summary>
        ///// <returns>Destination Port value</returns>
        //public static string GetKeyDestinationPort() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.destination.port"];
        //}

        ///// <summary>
        ///// Get Web Service Name value
        ///// </summary>
        ///// <returns>Web Service Name value</returns>
        //public static string GetKeyWebServiceName() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.web.service.name"];
        //}

        ///// <summary>
        ///// Get Web Soap Action value
        ///// </summary>
        ///// <returns>Web Soap Action value</returns>
        //public static string GetKeyWebSoapAction() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.web.soap.action"];
        //}

        ///// <summary>
        ///// Get Async attribute value
        ///// </summary>
        ///// <returns>Async attribute value</returns>
        //public static string GetKeyAsync() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.async"];
        //}

        ///// <summary>
        ///// Get domain Server
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetKeyDomainServer() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.domain.server"];
        //}

        ///// <summary>
        ///// Get VPILOGGER URI
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetVPILoggerUri() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.uri.vpi.logger"];
        //}

        ///// <summary>
        ///// Get VPILOGGER URI
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetVPICoreUri() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.uri.vpi.core"];
        //}

        ///// <summary>
        /////     Method responsable to get soap key api management.
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentSoapKeyAPI() {
        //    return ConfigurationManager.AppSettings["azure.key.soap.api"];
        //}

        ///// <summary>
        /////  Method responsable to get GPVM endpoint
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentGPVEndpoint() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.gpvm.endpoint"];
        //}

        //// #### 15/05/2024 - Implementação do envio para o MRS ## ///
        ///// <summary>
        /////      Method responsable to get MRS endpoint
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentMRSGPVEndpoint() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.mrs.endpoint"];
        //}

        ///// <summary>
        /////     Get file path NLOG.
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentAzureNLogFilePath() {
        //    return ConfigurationManager.AppSettings["environment.azure.nlog.filePath"];
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentAzureNlogMaxArchiveDays() {
        //    return ConfigurationManager.AppSettings["environment.azure.nlog.archive.max.archives"];
        //}

        //public static string GetServiceAddress() {
        //    return ConfigurationManager.AppSettings["service.address"];
        //}

        ///// <summary>
        ///// Get Web Soap Action value
        ///// </summary>
        ///// <returns>Web Soap Action value</returns>
        //public static string GetKeyWebSoapActionMrs() {
        //    return ConfigurationManager.AppSettings["environment.azure.key.web.soap.action.mrs"];
        //}

        ///// <summary>
        ///// Get dabase user key
        ///// </summary>
        ///// <returns>Database user key</returns>
        //public static string GetUserSendMrs() {
        //    return ConfigurationManager.AppSettings["azure.key.mrs.user.send"];
        //}

        ///// <summary>
        ///// Get dabase user key
        ///// </summary>
        ///// <returns>Database user key</returns>
        //public static string GetPasswordSendMrs() {
        //    return ConfigurationManager.AppSettings["azure.key.mrs.password.send"];
        //}

        ////------------------------------------------------------ Develop config --------------------------------------------------

        ///// <summary>
        ///// Get the Railway Weighting Backend IAM identity
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentKeyRWBackendIAMIdentity() {
        //    return ConfigurationManager.AppSettings["environment.key.rwbackend.iamidentity"];
        //}

        ///// <summary>
        ///// Get database name environment key
        ///// </summary>
        ///// <returns>Database name environment key</returns>
        //public static string GetEnvironmentKeyDBName() {
        //    return ConfigurationManager.AppSettings["environment.key.sql.db.name"];
        //}

        ///// <summary>
        ///// Get database instance name environment key
        ///// </summary>
        ///// <returns>Database instance name environment key</returns>
        //public static string GetEnvironmentKeyDBInstanceName() {
        //    return ConfigurationManager.AppSettings["environment.key.sql.db.instance"];
        //}

        ///// <summary>
        ///// Get database user environment key
        ///// </summary>
        ///// <returns>Database user key</returns>
        //public static string GetEnvironmentKeyDBUser() {
        //    return ConfigurationManager.AppSettings["environment.key.sql.db.user"];
        //}

        ///// <summary>
        ///// Get database user's password environment key
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetEnvironmentKeyDBUserPassword() {
        //    return ConfigurationManager.AppSettings["environment.key.sql.db.user.pass"];
        //}

        ///// <summary>
        ///// Get domain Server
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetEnvironmentKeyDomainServer() {
        //    return ConfigurationManager.AppSettings["environment.key.domain.server"];
        //}

        ///// <summary>
        ///// Get VPILOGGER URI
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetEnvironmentVPILoggerUri() {
        //    return ConfigurationManager.AppSettings["environment.key.uri.vpi.logger"];
        //}

        ///// <summary>
        ///// Get VPILOGGER URI
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetEnvironmentVPICoreUri() {
        //    return ConfigurationManager.AppSettings["environment.key.uri.vpi.core"];
        //}

        ///// <summary>
        ///// Get Async attribute value
        ///// </summary>
        ///// <returns>Async attribute value</returns>
        //public static string GetEnvironmentKeyAsync() {
        //    return ConfigurationManager.AppSettings["environment.key.async"];
        //}

        ///// <summary>
        ///// Get Web Soap Action value
        ///// </summary>
        ///// <returns>Web Soap Action value</returns>
        //public static string GetEnvironmentKeyWebSoapAction() {
        //    return ConfigurationManager.AppSettings["environment.key.web.soap.action"];
        //}

        ///// <summary>
        ///// Get Web Service Name value
        ///// </summary>
        ///// <returns>Web Service Name value</returns>
        //public static string GetEnvironmentKeyWebServiceName() {
        //    return ConfigurationManager.AppSettings["environment.key.web.service.name"];
        //}

        ///// <summary>
        ///// Get Destination Host value
        ///// </summary>
        ///// <returns>Destination Host value</returns>
        //public static string GetEnvironmentKeyDestinationHost() {
        //    return ConfigurationManager.AppSettings["environment.key.destination.host"];
        //}

        ///// <summary>
        ///// Get Destination Port value
        ///// </summary>
        ///// <returns>Destination Port value</returns>
        //public static string GetEnvironmentKeyDestinationPort() {
        //    return ConfigurationManager.AppSettings["environment.key.destination.port"];
        //}

        ///// <summary>
        ///// Get Web Method value
        ///// </summary>
        ///// <returns>Web Method value</returns>
        //public static string GetEnvironmentKeyWebMethod() {
        //    return ConfigurationManager.AppSettings["environment.key.web.method"];
        //}

        ///// <summary>
        ///// Get GPV Endpoint url
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentGPVEndpointDev() {
        //    return ConfigurationManager.AppSettings["environment.key.gpvm.endpoint"];
        //}

        //public static string GetEnvironmentSoapKeyAPIDev() {
        //    return ConfigurationManager.AppSettings["environment.key.soap.api"];
        //}

        ///// <summary>
        /////     Get file path NLOG.
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentDevVariableNLogFilePath() {
        //    return ConfigurationManager.AppSettings["environment.develop.nlog.filePath"];
        //}

        ///// <summary>
        /////     Get enviroment variable nlo max archive days
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnviromentDevNlogMaxArchiveDays() {
        //    return ConfigurationManager.AppSettings["environment.develop.nlog.archive.max.archives"];
        //}

        ///// <summary>
        ///// Get database user environment key
        ///// </summary>
        ///// <returns>Database user key</returns>
        //public static string GetEnvironmentDevKeyDBUser() {
        //    return ConfigurationManager.AppSettings["environment.develop.key.sql.db.user"];
        //}
        ///// <summary>
        ///// Get database user's password environment key
        ///// </summary>
        ///// <returns>Database user's password environment key</returns>
        //public static string GetEnvironmentDevKeyDBUserPassword() {
        //    return ConfigurationManager.AppSettings["environment.develop.key.sql.db.user.pass"];
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentDevKeySimSoftUser() {
        //    return ConfigurationManager.AppSettings["environment.develop.key.simsoft.user"];
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetEnvironmentDevKeySimSoftPass() {
        //    return ConfigurationManager.AppSettings["environment.develop.key.simsoft.pass"];
        //}

        ///// <summary>
        ///// Get Railway Weighting Backend Identity key
        ///// </summary>
        ///// <returns>Railway Weighting Backend Identity key</returns>
        //public static string GetDevKeyRWBackendIdentity() {
        //    return ConfigurationManager.AppSettings["develop.key.rwbackend.iamidentity"];
        //}
    }
}