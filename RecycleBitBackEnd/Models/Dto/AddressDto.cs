using RecycleBitBackEnd.Util.Filters;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Dto {

    /// <summary>
    ///     Class responsible per defining the AddressDto model
    /// </summary>
    public class AddressDTO {

        #region Attributes Properties

        /// <summary>
        ///     Attribute ZipCode
        /// </summary>
        [Required]
        [ValidateZipeCode]
        public string ZipCode { get; set; }

        /// <summary>
        ///     Attribute Longitude
        /// </summary>
        [Required]
        [ValidateLongitude]
        public double Longitude { get; set; }

        /// <summary>
        ///     Attribute Latitude
        /// </summary>
        [Required]
        [ValidateLatitude]
        public double Latitude { get; set; }

        /// <summary>
        ///     attribute Street
        /// </summary>
        [Required]
        public string Street { get; set; }

        /// <summary>
        ///     Attribute Number
        /// </summary>
        [Required]
        public int Number { get; set; }

        /// <summary>
        ///     Attribute Neighborhood
        /// </summary>
        [Required]
        public string Neighborhood { get; set; }

        /// <summary>
        ///     Attribute City
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        ///     Attribute State
        /// </summary>
        [Required]
        public string State { get; set; }

        /// <summary>
        ///     Attribute StateAbbr
        /// </summary>
        [Required]
        public string StateAbbr { get; set; }

        /// <summary>
        ///     Attribute Id
        /// </summary>
        public int Id { get; set; }

        //public COMPANY Company { get; set; }

        #endregion Attributes Properties
    }
}