using HarpiaCommon.Exceptions;
using RailwayWeighingBackEnd.models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Enums;
using VPIRailwayWeightingBackend.Models;

namespace RecycleBitBackEnd.Dao {

    /// <summary>
    /// Implementation of the IUsersDao interface
    /// </summary>
    public class UsersDaoImpl : IUsersDao {
        private readonly IPublicationBO auditBO;

        /// <summary>
        /// Default constructor for the UsersDaoImpl class.
        /// </summary>
        public UsersDaoImpl() { }

        /// <summary>
        /// Constructor with arguments for the UsersDaoImpl class.
        /// </summary>
        public UsersDaoImpl(IPublicationBO auditBO) {
            this.auditBO = auditBO ?? throw new ArgumentNullException("auditBO");
        }

        public void CreateUser(object user) {
            throw new NotImplementedException();
        }

        public void CreateUser(USUARIO user) {
            ContextEntities context  = new ContextEntities();
            context.USUARIO.Add(user);
            context.SaveChanges();
        }

        public void DeleteUser(int user) {
            throw new NotImplementedException();
        }

        public void EditUser(object user) {
            throw new NotImplementedException();
        }

        public void getAllUsers() {
            throw new NotImplementedException();
        }

        public void getUserById(int id) {
            throw new NotImplementedException();
        }
    }
}