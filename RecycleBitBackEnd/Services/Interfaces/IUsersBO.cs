using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models.Request;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    /// Class responsible for defining the interfaces of the Operator business layer
    /// </summary>
    public interface IUsersBO {

        #region Public Methods
        /// <summary>
        ///     Method interface responsible for creating a new user in the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserDTO CreateUser(CreateOrUpdateUserRequest user);

        /// <summary>
        ///     Method interface responsible for user login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserDTO Login(string email, string password);

        /// <summary>
        ///     Method interface to get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserDTO GetUserById(int userId);

        /// <summary>
        ///     Method interface responsible per getting all users
        /// </summary>
        /// <returns></returns>
        List<UserDTO> GetAllUsers();

        /// <summary>
        ///     Method interface responsible for deleting a user in the system boy id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string DeleteUser(int user);

        /// <summary>
        ///
        /// </summary>
        /// <param name="userIdEdit"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        UserDTO EditUser(int userIdEdit, CreateOrUpdateUserRequest request);
        #endregion
    }
}