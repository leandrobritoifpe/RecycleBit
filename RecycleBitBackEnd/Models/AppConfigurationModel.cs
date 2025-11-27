namespace RecycleBitBackEnd.Models {

    /// <summary>
    ///     Class instance object AppConfigurationModel
    /// </summary>
    public class AppConfigurationModel {

        /// <summary>
        ///     patternDateTime
        /// </summary>
        public string patternDateTime { get; set; }

        /// <summary>
        ///     patternDateTimeNonMili
        /// </summary>
        public string patternDateTimeNonMili { get; set; }

        /// <summary>
        ///     Instance SqlServer
        /// </summary>
        public string sqlServerInstanceName { get; set; }

        /// <summary>
        ///     Database SqlServer
        /// </summary>
        public string sqlServerDataBaseName { get; set; }

        /// <summary>
        ///     User SqlServer
        /// </summary>
        public string sqlServerUserName { get; set; }

        /// <summary>
        ///     Password SqlServer
        /// </summary>
        public string sqlServerPassword { get; set; }

        /// <summary>
        ///     Smtp
        /// </summary>
        public string smtpFrom { get; set; }

        /// <summary>
        ///     Host smtp
        /// </summary>
        public string smtpHost { get; set; }

        /// <summary>
        ///     port smtp
        /// </summary>
        public string smtpPort { get; set; }

        /// <summary>
        ///     Active Debug
        /// </summary>
        public string isDebug { get; set; }

        /// <summary>
        ///     Acive statr jobs
        /// </summary>
        public string startJobs { get; set; }

        /// <summary>
        ///     Enviroment
        /// </summary>
        public string appEnvironment { get; set; }

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public AppConfigurationModel() {
        }
    }
}