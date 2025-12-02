using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Exceptions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    /// Class responsible for implementing the INewUserBO interface
    /// </summary>
    public class UsersBOImpl : IUsersBO {
        private readonly IUsersDao usersDao;
        private readonly IRoleBO roleBo;
        private readonly IAddressBO addressBo;

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

        public string CreateUser(CreateUserRequest userRequest) {
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

            return DictionaryMessageView.USER_CREATE_SUCES;
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

        public UserDTO getUserById(int id) {
            USER user = usersDao.GetUserById(id);
            UserDTO userDto = ConvertObjectUserDTO(user);
            if (user == null) {
                throw new ProjectException(DictionaryError.INVALID_EMAIL_OR_PASSWORD);
            }
            return userDto;
        }

        private USER MappingDataUser(CreateUserRequest user, int adressId) {
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

        public void DeleteUser(int user) {
            throw new NotImplementedException();
        }

        public void EditUser(object user) {
            throw new NotImplementedException();
        }

        public void GettAllUsers() {
            throw new NotImplementedException();
        }

        private UserDTO ConvertObjectUserDTO(USER user) {
            UserDTO userDto = new() {
                Name = user.NAME,
                Status = user.STATUS,
                Email = user.EMAIL,
                RoleId = user.ROLE_ID,
                Role = user.ROLE.NAME,
                AddrresId = user.ADDRESS_ID,
                Id = user.USER_ID
            };
            return userDto;
        }
    }
}