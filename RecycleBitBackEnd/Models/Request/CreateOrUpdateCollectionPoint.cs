using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Class responsible per for creating or updating a Company
    /// </summary>
    public class CreateOrUpdateCollectionPoint {

        #region Attributes Properties

        /// <summary>
        ///     Attribute Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = DictionaryError.IS_VALUE_NOT_NULL)]
        public string Name { get; set; }

        /// <summary>
        ///     Attribute Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        [Required]
        public AddressDTO Address { get; set; }

        /// <summary>
        ///     Attribute Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        ///     Attribute CompanyCNPJ
        /// </summary>
        [Required]
        public string CompanyCNPJ { get; set; }

        #endregion Attributes Properties
    }
}