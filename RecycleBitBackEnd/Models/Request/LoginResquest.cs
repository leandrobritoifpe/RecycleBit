using RecycleBitBackEnd.Util.Filters;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Clkss responsible for encapsulating the login request
    /// </summary>
    public class LoginRequest {

        #region Attributes Properties
        /// <summary>
        ///     Attribute to validate email format
        /// </summary>
        [ValidateEmail]
        public string Email { get; set; }

        /// <summary>
        ///     Attribute to validate password format
        /// </summary>
        [ValidatePassword]
        public string Password { get; set; }
        #endregion
    }
}