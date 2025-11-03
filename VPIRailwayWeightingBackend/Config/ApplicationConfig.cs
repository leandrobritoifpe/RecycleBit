using Autofac;

namespace RecycleBitBackEnd.Config {

    /// <summary>
    /// Class to configure and parameterize the attributes, in order to avoid hard code.
    /// </summary>
    public static class ApplicationConfig {

       public static string APP_MODE { get; set; }

        /// <summary>
        /// Autofac container for dependency injection
        /// </summary>
        public static IContainer Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string SYNC_TABLES_CRON_EXPRESSION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string START_JOBS { get; set; }

        /// <summary>
        /// SQL username
        /// </summary>
        public static string SQL_USER { get; set; }

        /// <summary>
        /// SQL database name
        /// </summary>
        public static string SQL_DB_NAME { get; set; }

        /// <summary>
        /// SQL instance name
        /// </summary>
        public static string SQL_INSTANCE_NAME { get; set; }

        /// <summary>
        /// SQL user's password
        /// </summary>
        public static string SQL_USER_PASSWORD { get; set; }
    
        /// <summary>
        /// Days to search logs
        /// </summary>
        public static int DAYS_TO_SEARCH_LOGS = 1;

        /// <summary>
        /// Variabel de last datetime ut
        /// </summary>
        public static int LastDateTime { get; set; }

        /// <summary>
        /// Application name
        /// </summary> {GET
        public static string APPLICATION_NAME { get; set; }
        public static dynamic FileNLogPath { get; internal set; }
        public static dynamic NLogMaxArchiveDays { get; internal set; }
        public static string NLogMinLevel { get; internal set; }
        public static string NLogMaxLevel { get; internal set; }
    }
}