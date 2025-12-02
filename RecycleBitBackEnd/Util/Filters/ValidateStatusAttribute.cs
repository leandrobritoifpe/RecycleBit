using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///     Class responsible for validating a status according to security requirements
    /// </summary>
    public class ValidateStatusAttribute : ValidationAttribute {

        /// <summary>
        ///    Method responsible for initializing the ValidatePasswordAttribute class
        /// </summary>
        public ValidateStatusAttribute() {
            ErrorMessage = "O campo status deve está dentro do padrão esperado, true ou false";
        }

        /// <summary>
        ///     Method responsible for validating a password
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            bool status = (bool)value;
            return status;
        }
    }
}