using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Services.Interfaces;
using System;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    ///     Class responsible for implementing the IRoleBO interface
    /// </summary>
    public class RoleBOImpl : IRoleBO {

        #region Attributes Properties

        private readonly IRoleDao roleDao;

        #endregion Attributes Properties

        #region Constructors

        /// <summary>
        ///     Method responsible for implementing the IRoleBO interface
        /// </summary>
        public RoleBOImpl() {
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        ///     Method responsible for constructing the RoleBOImpl class with the specified IRoleDao.
        /// </summary>
        /// <param name="roleDao"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RoleBOImpl(IRoleDao roleDao) {
            this.roleDao = roleDao ?? throw new ArgumentNullException("roleDao");
        }

        /// <summary>
        ///     Class responsible for getting role by id
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public ROLE GetRoleById(int idRole) {
            return roleDao.GetRoleById(idRole);
        }

        #endregion Public Methods
    }
}