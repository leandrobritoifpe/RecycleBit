using RecycleBitBackEnd.Models;

namespace RecycleBitBackEnd.Dao.Interfaces {

    /// <summary>
    /// Class that defines the interfaces for comunicating with the Users table
    /// </summary>
    public interface IUsersDao {

        USER CreateUser(USER user);

        void EditUser(object user);

        void DeleteUser(int user);

        void GetAllUsers();

        void GetUserById(int id);

        /// <summary>
        ///     Method to get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        USER GetUserByEmail(string email);

        /// <summary>
        ///     Method to get user by CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        USER GetUserByCPF(string cpf);

        /// <summary>
        ///     Method interface responsible for user login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        USER Login(string email, string password);
    }
}