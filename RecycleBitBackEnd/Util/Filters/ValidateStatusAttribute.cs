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
            ErrorMessage = "O campo status deve está dentro do padrão esperado, entre 1 e 0";
        }

        /// <summary>
        ///     Method responsible for validating a password
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            int status = (int)value;
            if (status != 1 || status != 0)
                return false;

            return true;
        }
    }
}