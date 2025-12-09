using RecycleBitBackEnd.Models;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     Class responsible for defining the methods related to profile business operations
    /// </summary>
    public interface IRoleBO {

        #region Public Methods

        /// <summary>
        ///     Method interface responsible for getting role by id
        /// </summary>
        /// <param name="perfil"></param>
        /// <returns></returns>
        ROLE GetRoleById(int perfil);

        #endregion Public Methods
    }
}