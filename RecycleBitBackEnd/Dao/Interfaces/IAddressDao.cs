using RecycleBitBackEnd.Models;

namespace RecycleBitBackEnd.Dao.Interfaces {

    /// <summary>
    ///     Class responsible for defining the methods of the Address data access object.
    /// </summary>
    public interface IAddressDao {

        void SaveAddress(ADDRESS address);
    }
}