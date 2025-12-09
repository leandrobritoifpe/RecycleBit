using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///  Class responsible for validating a longitude decimal in degrees in the range [-180, 180].
    /// </summary>
    public class ValidateLongitudeAttribute : ValidationAttribute {

        /// <summary>
        ///     Constructor for ValidateLongitudeAttribute class
        /// </summary>
        public ValidateLongitudeAttribute() {
            ErrorMessage = "Longitude inválida. Informe um valor decimal entre -180 e +180.";
        }

        /// <summary>
        ///  Method responsible for validating a longitude in the range [-180, 180].
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            if (value == null)
                return false;

            var input = value.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var numberPattern = @"^[\+\-]?\s*\d+(?:[\,\.]\d+)?$";
            if (!Regex.IsMatch(input, numberPattern))
                return false;

            if (!TryParseInvariant(input, out var lon))
                return false;

            return lon >= -180.0 && lon <= 180.0;
        }

        /// <summary>
        ///   Method responsible for normalizing to double invariant (replaces comma with dot).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double? Normalize(string value) {
            if (!TryParseInvariant(value, out var lon))
                return null;

            return (lon >= -180.0 && lon <= 180.0) ? lon : (double?)null;
        }

        private static bool TryParseInvariant(string raw, out double result) {
            result = 0;
            if (string.IsNullOrWhiteSpace(raw))
                return false;

            var normalized = raw.Replace(" ", "").Replace(',', '.');
            return double.TryParse(normalized, NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out result);
        }
    }
}