using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///     Class responsible for validating a password according to security requirements
    /// </summary>
    public class ValidatePasswordAttribute : ValidationAttribute {

        /// <summary>
        ///    Method responsible for initializing the ValidatePasswordAttribute class
        /// </summary>
        public ValidatePasswordAttribute() {
            ErrorMessage = "A senha não atende aos requisitos mínimos de segurança.";
        }

        /// <summary>
        ///     Method responsible for validating a password
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            string password = (string)value;
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string @default = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            return Regex.IsMatch(value.ToString(), @default);
        }
    }
}