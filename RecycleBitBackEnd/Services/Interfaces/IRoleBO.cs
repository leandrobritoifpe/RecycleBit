using RecycleBitBackEnd.Models;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     Class responsible for defining the methods related to profile business operations
    /// </summary>
    public interface IRoleBO {

        ROLE GetRoleById(int perfil);
    }
}