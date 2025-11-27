namespace RecycleBitBackEnd.Config {

    /// <summary>
    ///     Class to configure and parameterize the attributes, in order to avoid hard code.
    /// </summary>
    public static class DictionaryAttribute {

        //ApplicationConfiguration
        /// <summary>
        ///     patternDateTime
        /// </summary>
        public static readonly string PATTERNDATETIME = "patternDateTime";

        /// <summary>
        ///     patternDateTimeNonMili
        /// </summary>
        public static readonly string PATTERNDATETIMENONMILI = "patternDateTimeNonMili";

        /// <summary>
        ///     sqlServerInstanceName
        /// </summary>
        public static readonly string SQLSERVERINSTANCENAME = "sqlServerInstanceName";

        /// <summary>
        /// sqlServerDataBaseName
        /// </summary>
        public static readonly string SQLSERVERDATABASENAME = "sqlServerDataBaseName";

        /// <summary>
        /// sqlServerUserName
        /// </summary>
        public static readonly string SQLSERVERUSERNAME = "sqlServerUserName";

        /// <summary>
        ///     sqlServerPassword
        /// </summary>
        public static readonly string SQLSERVERPASSWORD = "sqlServerPassword";

        /// <summary>
        ///     smtpFrom
        /// </summary>
        public static readonly string SMTPFROM = "smtpFrom";

        /// <summary>
        ///     smtpHost
        /// </summary>
        public static readonly string SMTPHOST = "smtpHost";

        /// <summary>
        ///     smtpPort
        /// </summary>
        public static readonly string SMTPPORT = "smtpPort";

        /// <summary>
        ///     isDebug
        /// </summary>
        public static readonly string ISDEBUG = "isDebug";

        /// <summary>
        ///     startJobs
        /// </summary>
        public static readonly string STARTJOBS = "startJobs";

        //General
        /// <summary>
        ///     Environment
        /// </summary>
        public static readonly string ENVIRONMENT = "Environment";
    }
}