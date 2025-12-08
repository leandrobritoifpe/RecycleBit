using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Dao.Interfaces {

    /// <summary>
    /// Class that defines the interfaces for comunicating with the Users table
    /// </summary>
    public interface IUsersDao {

        /// <summary>
        ///     Method responsible for creating a new user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        USER CreateUser(USER user);

        /// <summary>
        ///     Method responsible for getting all users in the system
        /// </summary>
        /// <returns></returns>
        List<USER> GetAllUsers();

        /// <summary>
        ///     Method to get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        USER GetUserById(int id);

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

        /// <summary>
        ///     Nethod responsible for editing a user in the system
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        USER EditUser(int userId, CreateOrUpdateUserRequest userRequest);

        /// <summary>
        ///     Method responsible for deleting a user in the system by id
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(int user);
    }
}