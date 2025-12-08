using RecycleBitBackEnd.Util.Enums;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Class responsible per encapsulating the request to get all users
    /// </summary>
    public class GetAllUserReques {

        #region Attributes Properties
        /// <summary>
        ///     Attribute that represents the applicant user ID
        /// </summary>
        [Required]
        public int UserIdApplicant { get; set; }

        /// <summary>
        ///     Attribute Role
        /// </summary>
        [Required]
        public Role Role { get; set; }
        #endregion
    }
}