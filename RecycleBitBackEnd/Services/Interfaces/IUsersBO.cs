using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models.Request;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    /// Class responsible for defining the interfaces of the Operator business layer
    /// </summary>
    public interface IUsersBO {

        string CreateUser(CreateUserRequest user);

        void EditUser(object user);

        void DeleteUser(int user);

        List<UserDTO> GettAllUsers();

        UserDTO getUserById(int userId);

        UserDTO Login(string email, string password);
    }
}