using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///     Class responsible for validating a latitude decimal in degrees in the range [-90, 90].
    /// </summary>
    public class ValidateLatitudeAttribute : ValidationAttribute {

        public ValidateLatitudeAttribute() {
            ErrorMessage = "Latitude inválida. Informe um valor decimal entre -90 e +90.";
        }

        /// <summary>
        ///     Class responsible for validating a latitude in the range [-90, 90].
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            if (value == null)
                return false;

            string input = value.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string numberPattern = @"^[\+\-]?\s*\d+(?:[\,\.]\d+)?$";
            if (!Regex.IsMatch(input, numberPattern))
                return false;

            if (!TryParseInvariant(input, out var lat))
                return false;

            return lat >= -90.0 && lat <= 90.0;
        }

        /// <summary>
        ///     Normaliza para double invariant (troca vírgula por ponto).
        ///     Retorna null se inválido ou fora de faixa.
        /// </summary>
        public static double? Normalize(string value) {
            if (!TryParseInvariant(value, out var lat))
                return null;

            return (lat >= -90.0 && lat <= 90.0) ? lat : (double?)null;
        }

        private static bool TryParseInvariant(string raw, out double result) {
            result = 0;
            if (string.IsNullOrWhiteSpace(raw))
                return false;

            string normalized = raw.Replace(" ", "").Replace(',', '.');
            return double.TryParse(normalized, NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out result);
        }
    }
}