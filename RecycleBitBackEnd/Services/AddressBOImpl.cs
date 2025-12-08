using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Services.Interfaces;
using System;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    ///     Class responsible for implementing the IAddressBO interface.
    /// </summary>
    public class AddressBOImpl : IAddressBO {
        #region Attributes Properties
        private readonly IAddressDao addressDao;
        #endregion

        #region Constructors
        public AddressBOImpl() {
        }

        public AddressBOImpl(IAddressDao addressDao) {
            this.addressDao = addressDao ?? throw new ArgumentNullException("addressDao");
        }
        #endregion

        #region Public Methods
        /// <summary>
        ///     Method responsible for saving an address in the database.
        /// </summary>
        /// <param name="addressDto"></param>
        /// <returns></returns>
        public ADDRESS SaveAddress(AddressDto addressDto) {
            ADDRESS address = MappingAddressObject(addressDto);
            return addressDao.SaveAddress(address);
        }
        #endregion

        #region Private Methods
        /// <summary>
        ///     Method responsible for mapping an AddressDto object to an ADDRESS entity.
        /// </summary>
        /// <param name="addressDto"></param>
        /// <returns></returns>
        private ADDRESS MappingAddressObject(AddressDto addressDto) {
            ADDRESS address = new() {
                CITY = addressDto.City,
                NEIGHBORHOOD = addressDto.Neighborhood,
                NUMBER = addressDto.Number,
                STATE = addressDto.State,
                STATE_ABBR = addressDto.StateAbbr,
                LATITUDE = (decimal?)addressDto.Latitude,
                LONGITUDE = (decimal?)addressDto.Longitude,
                STREET = addressDto.Street,
                ZIP_CODE = addressDto.ZipCode,
            };
            return address;
        }
        #endregion
    }
}