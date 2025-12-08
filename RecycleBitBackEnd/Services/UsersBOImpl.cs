using log4net;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    /// Class responsible for implementing the INewUserBO interface
    /// </summary>
    public class UsersBOImpl : IUsersBO {

        #region Attributes Properties
        private readonly ILog Logger = LogManager.GetLogger(typeof(UsersBOImpl));
        private readonly IUsersDao usersDao;
        private readonly IRoleBO roleBo;
        private readonly IAddressBO addressBo;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for the NewUserBOImpl class.
        /// </summary>
        public UsersBOImpl() { }

        /// <summary>
        /// Constructor for the NewUserBOImpl class that initializes the logger and DAO.
        /// </summary>
        /// <param name="usersDao"></param>
        public UsersBOImpl(IUsersDao usersDao, IAddressBO addressBo, IRoleBO roleBo) {
            this.usersDao = usersDao ?? throw new ArgumentNullException("usersDao");
            this.addressBo = addressBo ?? throw new ArgumentNullException("addressBo");
            this.roleBo = roleBo ?? throw new ArgumentNullException("roleBo");
        }
        #endregion

        #region Public Methods

        /// <summary>
        ///     Method responsible for creating a new user in the system
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        public UserDTO CreateUser(CreateOrUpdateUserRequest userRequest) {
            ROLE role = roleBo.GetRoleById(userRequest.RoleId);

            if (role == null)
                throw new ProjectException(String.Format(DictionaryError.ID_ROLE_NO_REFERENCES, userRequest.RoleId));

            if (usersDao.GetUserByCPF(userRequest.CPF) != null)
                throw new ProjectException(DictionaryError.CPF_EXIST_IN_DATABASE);

            if (usersDao.GetUserByEmail(userRequest.Email) != null)
                throw new ProjectException(DictionaryError.EMAIL_EXIST_IN_DATABASE);

            ADDRESS Adrres = addressBo.SaveAddress(userRequest.Address);

            USER userInsert = MappingDataUser(userRequest, Adrres.ADDRESS_ID);

            USER userCadaster = usersDao.CreateUser(userInsert);

            if (userCadaster == null)
                throw new ProjectException(String.Format(DictionaryError.ID_ROLE_NO_REFERENCES, userRequest.RoleId));

            return ConvertObjectUserDTO(userCadaster);
        }

        /// <summary>
        ///     Method responsible for user login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        public UserDTO Login(string email, string password) {
            string hashedPassword = GenerateMD5(password);
            USER user = usersDao.Login(email, hashedPassword);
            UserDTO userDto = ConvertObjectUserDTO(user);
            if (user == null) {
                throw new ProjectException(DictionaryError.INVALID_EMAIL_OR_PASSWORD);
            }
            return userDto;
        }

        /// <summary>
        ///     Method responsible for getting user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        public UserDTO GetUserById(int id) {
            USER user = usersDao.GetUserById(id);
            UserDTO userDto = ConvertObjectUserDTO(user);
            if (user == null) {
                throw new ProjectException(DictionaryError.INVALID_EMAIL_OR_PASSWORD);
            }
            return userDto;
        }

        /// <summary>
        ///     Method responsible for getting all users
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetAllUsers() {
            List<UserDTO> listUserDto = new();
            List<USER> listUser = usersDao.GetAllUsers();
            foreach (USER user in listUser) {
                try {
                    listUserDto.Add(ConvertObjectUserDTO(user));
                } catch (Exception ex) {
                    throw;
                }
            }
            return listUserDto;
        }

        /// <summary>
        ///     Method responsible for deleting a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string DeleteUser(int user) {
            usersDao.DeleteUser(user);
            return DictionaryMessageView.DELETE_USER_SUCESS;
        }


        public UserDTO EditUser(int userIdEdit, CreateOrUpdateUserRequest request) {
            request.CPF = request.CPF.Replace(".", "").Replace("-", "");
            request.Password = GenerateMD5(request.Password);
            USER userEdit = usersDao.EditUser(userIdEdit, request);
            return ConvertObjectUserDTO(userEdit);
        }
        #endregion

        #region Private Methods

        private USER MappingDataUser(CreateOrUpdateUserRequest user, int adressId) {
            USER userInsert = new() {
                CPF = user.CPF.Replace(".", "").Replace("-", ""),
                EMAIL = user.Email,
                PASSWORD = GenerateMD5(user.Password),
                NAME = user.Name,
                PHONE = user.Phone,
                BIRTH_DATE = user.DateNasc,
                STATUS = user.Status,
                ADDRESS_ID = adressId,
                ROLE_ID = user.RoleId
            };
            return userInsert;
        }

        private static string GenerateMD5(string senha) {
            if (string.IsNullOrWhiteSpace(senha))
                return string.Empty;

            using (MD5 md5 = MD5.Create()) {
                byte[] inputBytes = Encoding.UTF8.GetBytes(senha);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
        private UserDTO ConvertObjectUserDTO(USER user) {
            UserDTO userDto = new() {
                Name = user.NAME,
                Status = user.STATUS,
                Email = user.EMAIL,
                RoleId = user.ROLE_ID,
                Role = user.ROLE.NAME,
                AddressId = user.ADDRESS_ID,
                Id = user.USER_ID,
                Address = new() {
                    Id = user.ADDRESS.ADDRESS_ID,
                    Street = user.ADDRESS.STREET,
                    Number = user.ADDRESS.NUMBER,
                    Neighborhood = user.ADDRESS.NEIGHBORHOOD,
                    City = user.ADDRESS.CITY,
                    State = user.ADDRESS.STATE,
                    ZipCode = user.ADDRESS.ZIP_CODE,
                    Longitude = (double)(user.ADDRESS.LONGITUDE ?? 0),
                    Latitude = (double)(user.ADDRESS.LATITUDE ?? 0)
                }
            };
            return userDto;
        }
        #endregion
    }
}