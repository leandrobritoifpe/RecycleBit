using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Models.Response;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util;
using RecycleBitBackEnd.Util.Exceptions;
using System;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    /// Class responsible for implementing the INewUserBO interface
    /// </summary>
    public class UsersBOImpl : IUsersBO {

        #region Attributes Properties

        private readonly IUsersDao usersDao;
        private readonly IRoleBO roleBo;
        private readonly IAddressBO addressBo;
        private readonly ICompanyBO companyBo;

        #endregion Attributes Properties

        #region Constructors

        /// <summary>
        /// Default constructor for the NewUserBOImpl class.
        /// </summary>
        public UsersBOImpl() { }

        /// <summary>
        /// Constructor for the NewUserBOImpl class that initializes the logger and DAO.
        /// </summary>
        /// <param name="usersDao"></param>
        public UsersBOImpl(IUsersDao usersDao, IAddressBO addressBo, IRoleBO roleBo, ICompanyBO companyBo) {
            this.usersDao = usersDao ?? throw new ArgumentNullException("usersDao");
            this.addressBo = addressBo ?? throw new ArgumentNullException("addressBo");
            this.roleBo = roleBo ?? throw new ArgumentNullException("roleBo");
            this.companyBo = companyBo ?? throw new ArgumentNullException("companyBo");
        }

        #endregion Constructors

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
        public LoginResponse Login(string email, string password) {
            LoginResponse login = new() {
                Company = new(),
                User = new()
            };

            string hashedPassword = Encriptor.GenerateMD5(password);
            USER user = usersDao.Login(email, hashedPassword);

            if (user != null) {
                login.User = ConvertObjectUserDTO(user);
                return login;
            } else {
                login.Company = companyBo.Login(email, hashedPassword);
                if (login.Company == null)
                    throw new ProjectException(DictionaryError.INVALID_EMAIL_OR_PASSWORD);
            }
            return login;
        }

        /// <summary>
        ///     Method responsible for getting user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        public UserDTO GetUserById(int id) {
            USER user = usersDao.GetUserById(id);
            if (user == null) {
                throw new ProjectException(DictionaryError.INVALID_EMAIL_OR_PASSWORD);
            }
            return ConvertObjectUserDTO(user);
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
                    throw ex;
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

        /// <summary>
        ///     Method responsible for editing a user in the system
        /// </summary>
        /// <param name="userIdEdit"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        public UserDTO EditUser(int userIdEdit, CreateOrUpdateUserRequest request) {
            request.CPF = request.CPF.Replace(".", "").Replace("-", "");
            request.Password = Encriptor.GenerateMD5(request.Password);
            USER userEdit = usersDao.EditUser(userIdEdit, request);
            return ConvertObjectUserDTO(userEdit);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Private method responsible for mapping the data from the request to the USER model
        /// </summary>
        /// <param name="user"></param>
        /// <param name="adressId"></param>
        /// <returns></returns>
        private USER MappingDataUser(CreateOrUpdateUserRequest user, int adressId) {
            USER userInsert = new() {
                CPF = user.CPF.Replace(".", "").Replace("-", ""),
                EMAIL = user.Email,
                PASSWORD = Encriptor.GenerateMD5(user.Password),
                NAME = user.Name,
                PHONE = user.Phone,
                BIRTH_DATE = user.DateNasc,
                STATUS = user.Status,
                ADDRESS_ID = adressId,
                ROLE_ID = user.RoleId
            };
            return userInsert;
        }

        /// <summary>
        ///     Private method responsible for converting the USER object to UserDTO
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        #endregion Private Methods
    }
}