using RecycleBitBackEnd.Models.Dto;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     Class responsible for defining the methods of the Address business object.
    /// </summary>
    public interface IAddressBO {

        /// <summary>
        ///     Method responsible for mapping an AddressDto object to an ADDRESS entity.
        /// </summary>
        /// <param name="addressDto"></param>
        /// <returns></returns>
        void SaveAddress(AddressDto addressDto);
    }
}