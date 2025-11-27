using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Services.Interfaces;
using System;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    ///     Class responsible for implementing the IRoleBO interface
    /// </summary>
    public class RoleBOImpl : IRoleBO {
        private readonly IRoleDao roleDao;

        public RoleBOImpl() {
        }

        public RoleBOImpl(IRoleDao roleDao) {
            this.roleDao = roleDao ?? throw new ArgumentNullException("roleDao");
        }

        public ROLE GetRoleById(int idRole) {
            return roleDao.GetRoleById(idRole);
        }
    }
}