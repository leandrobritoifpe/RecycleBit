using RecycleBitBackEnd.Util.Enums;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Class responsible per defining the EditUserRequest model
    /// </summary>
    public class EditCompanyRequest {

        #region Attributes Properties
        /// <summary>
        ///     Attribute UserRequest
        /// </summary>
        [Required]
        public CreateOrUpdateCompanyRequest Request { get; set; }

        /// <summary>
        ///     Attribute Role
        /// </summary>
        [Required]
        public Role Role { get; set; }

        /// <summary>
        ///     At
        /// </summary>
        [Required]
        public string cnpjApplicant { get; set; }

        /// <summary>
        ///     Aattribute UserIdEdit
        /// </summary>
        [Required]
        public string cnpjEdit { get; set; }
        #endregion
    }
}