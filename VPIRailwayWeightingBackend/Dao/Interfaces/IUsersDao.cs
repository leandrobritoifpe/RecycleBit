using RailwayWeighingBackEnd.models.dto;
using System.Collections.Generic;
using RecycleBitBackEnd.Models;
using VPIRailwayWeightingBackend.Models;

namespace RecycleBitBackEnd.Dao.Interfaces {

    /// <summary>
    /// Class that defines the interfaces for comunicating with the Users table
    /// </summary>
    public interface IUsersDao {

        void CreateUser(USUARIO user);
        void EditUser(object user);
        void DeleteUser(int user);
        void getAllUsers();
        void getUserById(int id);

    }
}