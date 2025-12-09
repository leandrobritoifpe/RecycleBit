using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Util.Filters;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Class responsible per for creating or updating a Company
    /// </summary>
    public class CreateOrUpdateCompanyRequest {

        #region Attributes Properties

        /// <summary>
        ///     Attribute Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = DictionaryError.IS_VALUE_NOT_NULL)]
        public string CorporateName { get; set; }

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
        ///     Attribute RoleId
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        /// <summary>
        ///     Attribute Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Attribute CPF
        /// </summary>
        [Required]
        [ValidateCNPJ]
        public string CNPJ { get; set; }

        /// <summary>
        ///     Attribute Address
        /// </summary>
        [Required]
        public AddressDTO Address { get; set; }

        /// <summary>
        ///     Attribute Responsible
        /// </summary>
        [Required]
        public string Responsible { get; set; }

        /// <summary>
        ///     Attribute Status
        /// </summary>
        public bool Status { get; set; }

        #endregion Attributes Properties
    }
}