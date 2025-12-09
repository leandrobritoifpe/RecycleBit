using RecycleBitBackEnd.Models.Dto;

namespace RecycleBitBackEnd.models.dto {

    /// <summary>
    /// Information about the user
    /// </summary>
    public class UserDTO {

        #region Attributes Properties
        /// <summary>
        /// User ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User type
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User status is active or not
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Indicates if the user is a weighing user
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        ///     Attribute Address
        /// </summary>
        public AddressDTO Address { get; set; }
        #endregion
    }
}