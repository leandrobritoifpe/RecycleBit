using RecycleBitBackEnd.Models;

namespace RecycleBitBackEnd.Dao.Interfaces {

    /// <summary>
    ///     Class responsible for defining the IRoleDao interface
    /// </summary>
    public interface IRoleDao {

        /// <summary>
        ///     Gets the user profile based on the provided profile identifier.
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        ROLE GetRoleById(int idRole);
    }
}