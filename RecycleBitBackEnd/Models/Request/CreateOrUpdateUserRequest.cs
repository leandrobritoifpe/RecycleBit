using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Util.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Class responsible per for creating or updating a user
    /// </summary>
    public class CreateOrUpdateUserRequest {

        #region Attributes Properties

        /// <summary>
        ///     Attribute Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = DictionaryError.IS_VALUE_NOT_NULL)]
        public string Name { get; set; }

        /// <summary>
        ///     Attribute Password
        /// </summary>
        [Required]
        [ValidatePassword]
        public string Password { get; set; }

        /// <summary>
        ///     Attribute Email
        /// </summary>
        [Required]
        [ValidateEmail]
        public string Email { get; set; }

        /// <summary>
        ///     Attribute Status
        /// </summary>
        [ValidateStatus]
        public bool Status { get; set; }

        /// <summary>
        ///     Attribute RoleId
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        /// <summary>
        ///     Attribute Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Attribute DateNasc
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2500")]
        public DateTime DateNasc { get; set; }

        /// <summary>
        ///     Attribute CPF
        /// </summary>
        [Required]
        [ValidateCPF]
        public string CPF { get; set; }

        /// <summary>
        ///     Attribute Address
        /// </summary>
        public AddressDto Address { get; set; }
        #endregion
    }
}