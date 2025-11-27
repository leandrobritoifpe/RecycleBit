using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using System.Linq;

namespace RecycleBitBackEnd.Dao {

    /// <summary>
    ///     Class responsible for implementing the IRoleDao interface
    /// </summary>
    public class RoleDaoImpl : IRoleDao {

        public ROLE GetRoleById(int perfil) {
            RecycleBitEntities context = new RecycleBitEntities();
            ROLE role = context.ROLE.Where(r => r.ROLE_ID == perfil).FirstOrDefault();
            return role;
        }
    }
}