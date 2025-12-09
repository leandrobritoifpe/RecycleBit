using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    /// <summary>
    ///     Class responsible per encapsulating the data for editing a collection point
    /// </summary>
    public class EditColletionPointRequest {

        #region Attributes Properties

        /// <summary>
        ///     Attribute CompanyCNPJ
        /// </summary>
        [Required]
        public string CompanyCNPJ { get; set; }

        /// <summary>
        ///     Attribute Request
        /// </summary>
        [Required]
        public CreateOrUpdateCollectionPoint Request { get; set; }

        /// <summary>
        ///     Attribute IdPoint
        /// </summary>
        [Required]
        public int IdPoint { get; set; }

        #endregion Attributes Properties
    }
}