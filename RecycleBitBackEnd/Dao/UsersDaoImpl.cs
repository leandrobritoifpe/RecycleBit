using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Util.Exceptions;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace RecycleBitBackEnd.Dao {

    /// <summary>
    /// Implementation of the IUsersDao interface
    /// </summary>
    public class UsersDaoImpl : IUsersDao {

        #region Constructors
        /// <summary>
        /// Default constructor for the UsersDaoImpl class.
        /// </summary>
        public UsersDaoImpl() { }
        #endregion

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

            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        /// <summary>
        ///     Method responsible for deleting a user in the database
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(int user) {
            try {
                RecycleBitEntities context = new();
                context.USER.Where(u => u.USER_ID == user).FirstOrDefault().STATUS = false;
                context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <remarks>This method queries the database and returns all user records as a list.  The
        /// returned list will be empty if no users are found.</remarks>
        /// <returns>A <see cref="List{T}"/> of <see cref="USER"/> objects representing all users in the database. If no users
        /// are found, an empty list is returned.</returns>
        public List<USER> GetAllUsers() {
            RecycleBitEntities context = new();
            return context.USER.Where(user => user.STATUS == true).ToList();
        }

        /// <summary>
        ///     Method responsible for getting user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public USER GetUserById(int id) {
            RecycleBitEntities context = new();
            USER user = context.USER.Where(user => user.USER_ID == id && user.STATUS == true).FirstOrDefault();
            return user;
        }

        /// <summary>
        ///     Method to get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public USER GetUserByEmail(string email) {
            RecycleBitEntities context = new();
            USER userSearched = context.USER.Where(user => user.EMAIL == email && user.STATUS == true).FirstOrDefault();
            return userSearched;
        }

        /// <summary>
        ///     Method responsible for getting user by CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public USER GetUserByCPF(string cpf) {
            RecycleBitEntities context = new();
            USER userSearched = context.USER.Where(user => user.CPF == cpf && user.STATUS == true).FirstOrDefault();
            return userSearched;
        }

        /// <summary>
        ///     Method responsible for user login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public USER Login(string email, string password) {
            RecycleBitEntities context = new();
            USER userSearched = context.USER.Where(user => user.EMAIL == email && user.PASSWORD == password && user.STATUS == true).FirstOrDefault();
            return userSearched;
        }

        /// <summary>
        ///     Method responsible for editing a user in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRequest"></param>
        public USER EditUser(int userId, CreateOrUpdateUserRequest userRequest) {
            try {
                RecycleBitEntities context = new();
                USER userEdit = context.USER.Where(user => user.USER_ID == userId && user.STATUS == true).FirstOrDefault();
                if (userEdit != null) {
                    userEdit.NAME = userRequest.Name;
                    userEdit.CPF = userRequest.CPF;
                    userEdit.PHONE = userRequest.Phone;
                    userEdit.STATUS = userRequest.Status;
                    userEdit.ROLE_ID = userRequest.RoleId;
                    userEdit.ADDRESS.LONGITUDE = (decimal)(userRequest.Address.Longitude);
                    userEdit.ADDRESS.LATITUDE = (decimal)(userRequest.Address.Latitude);
                    userEdit.ADDRESS.STATE_ABBR = userRequest.Address.StateAbbr;
                    userEdit.ADDRESS.STATE = userRequest.Address.State;
                    userEdit.ADDRESS.CITY = userRequest.Address.City;
                    userEdit.ADDRESS.NEIGHBORHOOD = userRequest.Address.Neighborhood;
                    userEdit.ADDRESS.STREET = userRequest.Address.Street;
                    userEdit.ADDRESS.NUMBER = userRequest.Address.Number;
                    userEdit.ADDRESS.STREET = userRequest.Address.Street;
                    userEdit.ADDRESS.ZIP_CODE = userRequest.Address.ZipCode;
                    context.SaveChanges();
                }
                return userEdit;
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }
    }
}