using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    /// Class responsible for defining the interfaces of the Operator business layer
    /// </summary>
    public interface IUsersBO {

        string CreateUser(CreateUserRequest user);

        void EditUser(object user);

        void DeleteUser(int user);

        void getAllUsers();

        void getUserById(int id);

        USER Login(string email, string password);
    }
}