namespace RecycleBitBackEnd.Models.Dto {

    /// <summary>
    ///     Class responsible per the Data Transfer Object (DTO) for Company
    /// </summary>
    public class CollectionPointDTO {

        #region Attributes Properties

        /// <summary>
        ///     Attribute Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Attribute Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Attribute Address
        /// </summary>
        public AddressDTO Address { get; set; }

        /// <summary>
        ///     Attribute Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        ///     Attribute Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Atttribute CompanyCNPJ
        /// </summary>
        public string CompanyCNPJ { get; set; }

        #endregion Attributes Properties
    }
}