namespace RecycleBitBackEnd.Models.Dto {

    /// <summary>
    ///     Class responsible per the Data Transfer Object (DTO) for Company
    /// </summary>
    public class CompanyDTO {

        #region Attributes Properties

        /// <summary>
        ///     Attribute Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Attribute CorporateName
        /// </summary>
        public string CorporateName { get; set; }

        /// <summary>
        ///     Attribute Cnpj
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     Responsible for the company
        /// </summary>
        public string Responsible { get; set; }

        /// <summary>
        ///     Attribute Address
        /// </summary>
        public AddressDTO Address { get; set; }

        /// <summary>
        ///     Attribute Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        ///     Attribute AddressId
        /// </summary>
        public int AddressId { get; set; }
        #endregion
    }
}