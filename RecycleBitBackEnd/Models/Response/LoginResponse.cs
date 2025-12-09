using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models.Dto;

namespace RecycleBitBackEnd.Models.Response {

    /// <summary>
    ///     Class responsible per representing the response of a login operation
    /// </summary>
    public class LoginResponse {

        /// <summary>
        ///     Attribute Company
        /// </summary>
        public CompanyDTO Company { get; set; }

        /// <summary>
        ///     Attribute User
        /// </summary>
        public UserDTO User { get; set; }
    }
}