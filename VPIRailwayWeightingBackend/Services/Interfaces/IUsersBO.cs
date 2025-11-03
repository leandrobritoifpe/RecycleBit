using RailwayWeighingBackEnd.models.dto;
using System.Collections.Generic;
using RecycleBitBackEnd.Models;
using VPIRailwayWeightingBackend.Models.Request;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    /// Class responsible for defining the interfaces of the Operator business layer
    /// </summary>
    public interface IUsersBO {
        string CreateUser(UserRequest user);
        void EditUser(object user);
        void DeleteUser(int user);
        void getAllUsers();
        void getUserById(int id);
    }
}