using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Services.Interfaces;
using System;
using System.Linq;

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

        /// <summary>
        ///     Method responsible for creating a new user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public USER CreateUser(USER user) {
            try {
                RecycleBitEntities context = new();

                context.USER.Add(user);
                context.SaveChanges();

                USER userCadaster = context.USER.Where(u => u.EMAIL == user.EMAIL && u.CPF == user.CPF).FirstOrDefault();
                return userCadaster;

            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
                foreach (var validationErrors in ex.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        Console.WriteLine($"Propriedade: {validationError.PropertyName} Erro: {validationError.ErrorMessage}");
                    }
                }
                throw; // ou retorne uma mensagem mais amigável
            }
        }

        public void DeleteUser(int user) {
            throw new NotImplementedException();
        }

        public void EditUser(object user) {
            throw new NotImplementedException();
        }

        public void GetAllUsers() {
            throw new NotImplementedException();
        }

        public USER GetUserById(int id) {
            RecycleBitEntities context = new();
            USER user = context.USER.Where(u => u.USER_ID == id).FirstOrDefault();
            return user;
        }

        public USER GetUserByEmail(string email) {
            RecycleBitEntities context = new();
            USER userSearched = context.USER.Where(us => us.EMAIL == email).FirstOrDefault();
            return userSearched;
        }

        public USER GetUserByCPF(string cpf) {
            RecycleBitEntities context = new();
            USER userSearched = context.USER.Where(us => us.CPF == cpf).FirstOrDefault();
            return userSearched;
        }

        public USER Login(string email, string password) {
            RecycleBitEntities context = new();
            USER userSearched = context.USER.Where(us => us.EMAIL == email && us.PASSWORD == password).FirstOrDefault();
            return userSearched;
        }
    }
}