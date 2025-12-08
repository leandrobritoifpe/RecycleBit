using RecycleBitBackEnd.Util.Enums;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Class responsible per defining the EditUserRequest model
    /// </summary>
    public class EditUserRequest {

        #region Attributes Properties
        /// <summary>
        ///     Attribute UserRequest
        /// </summary>
        [Required]
        public CreateOrUpdateUserRequest UserRequest { get; set; }

        /// <summary>
        ///     Attribute Role
        /// </summary>
        [Required]
        public Role Role { get; set; }

        /// <summary>
        ///     At
        /// </summary>
        [Required]
        public int UserIdApplicant { get; set; }

        /// <summary>
        ///     Aattribute UserIdEdit
        /// </summary>
        [Required]
        public int UserIdEdit { get; set; }
        #endregion
    }
}