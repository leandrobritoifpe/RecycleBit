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
        private readonly IAddressDao addressDao;

        public AddressBOImpl() {
        }

        public AddressBOImpl(IAddressDao addressDao) {
            this.addressDao = addressDao ?? throw new ArgumentNullException("addressDao");
        }

        public ADDRESS SaveAddress(AddressDto addressDto) {
            ADDRESS address = MappingAddressObject(addressDto);
            return addressDao.SaveAddress(address);
        }

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
    }
}