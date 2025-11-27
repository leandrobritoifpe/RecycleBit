using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;

namespace RecycleBitBackEnd.Dao {

    public class AddressDaoImpl : IAddressDao {

        public void SaveAddress(ADDRESS address) {
            RecycleBitEntities context = new RecycleBitEntities();
            context.ADDRESS.Add(address);
            context.SaveChanges();
        }
    }
}