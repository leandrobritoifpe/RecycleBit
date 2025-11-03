using RecycleBitBackEnd.Config;

namespace System {
    /// <summary>
    /// A Class to extend DateTime functionalities
    /// </summary>
    public static class DateTimeExtension {

        /// <summary>
        /// Convert DateTime to Brazil DateTime
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime ToBrazilDateTime(this DateTime value) {
            return TimeZoneInfo.ConvertTime(value, BusinessConfig.BRAZIL_TIMEZONE);
        }

        /// <summary>
        /// Convert a DateTime to Brazil DateTime or not but includes the Brazil TimeOffSet
        /// </summary>
        /// <returns>DateTimeOffset</returns>
        public static DateTimeOffset ToBrazilDateTimeOffset(this DateTime value, bool internalConvertTimeZone = true) {
            DateTime date = internalConvertTimeZone ? TimeZoneInfo.ConvertTime(value, BusinessConfig.BRAZIL_TIMEZONE) : value;
            return new DateTimeOffset(date, TimeSpan.FromHours(-3));
        }
    }
}