using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///    Classe responsible pertaining to validating an email address
    /// </summary>
    public class ValidateEmailAttribute : ValidationAttribute {

        /// <summary>
        ///     Method responsible for initializing the ValidateEmail class
        /// </summary>
        public ValidateEmailAttribute() {
            ErrorMessage = "E-mail não atende o padrão exigido para ser considerado um correio postal";
        }

        /// <summary>
        ///     Method responsible for validating an email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            string email = (string)value;
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string @default = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, @default, RegexOptions.IgnoreCase);
        }
    }
}