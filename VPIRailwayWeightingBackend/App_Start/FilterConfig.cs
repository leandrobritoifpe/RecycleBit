using System.Web.Mvc;

namespace RecycleBitBackEnd {

    /// <summary>
    /// Filter configuration class
    /// </summary>
    public static class FilterConfig {

        /// <summary>
        /// Register global filters
        /// </summary>
        /// <param name="filters">Global filters collection</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}