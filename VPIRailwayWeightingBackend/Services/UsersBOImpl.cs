using HarpiaCommon.Services.Interfaces;
using RailwayWeighingBackEnd.models.dto;
using System;
using System.Collections.Generic;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Services.Interfaces;
using VPIRailwayWeightingBackend.Models.Request;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using VPIRailwayWeightingBackend.Models;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    /// Class responsible for implementing the INewUserBO interface
    /// </summary>
    public class UsersBOImpl : IUsersBO {
        private readonly IUsersDao usersDao;

        /// <summary>
        /// Default constructor for the NewUserBOImpl class.
        /// </summary>
        public UsersBOImpl() { }

        /// <summary>
        /// Constructor for the NewUserBOImpl class that initializes the logger and DAO.
        /// </summary>
        /// <param name="usersDao"></param>
        public UsersBOImpl(IUsersDao usersDao) {
            this.usersDao = usersDao ?? throw new ArgumentNullException("usersDao");
        }

        public string CreateUser(UserRequest user) {

          if(ValidateCPF(user.CPF)) {
               user.CPF = user.CPF.Replace(".", "").Replace("-", "").Trim();
          }

          if(ValidateEmail(user.Email)) {
               user.Email = user.Email.Trim();
          }

          if (ValidatePassowrd(user.password)) {
                user.password = GenerateMD5(user.password);
          }

           usersDao.CreateUser(MapToUserEntity(user));

            return "usuário criado com sucesso";
        }

        private bool ValidateCPF(string cpf) {

            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            
            cpf = Regex.Replace(cpf, "[^0-9]", "");

           
            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

           
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string digitosCalculados = digito1.ToString() + digito2.ToString();

            return cpf.EndsWith(digitosCalculados);

        }

        private bool ValidateEmail(string email) {

            if (string.IsNullOrWhiteSpace(email))
                return false;

            string @default = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, @default, RegexOptions.IgnoreCase);

        }

        public static bool ValidatePassowrd(string password) {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string @default = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            return Regex.IsMatch(password, @default);
        }



        public static string GenerateMD5(string senha) {
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

        private USUARIO MapToUserEntity(UserRequest userRequest) {
            return new USUARIO {
                NOME = userRequest.Name,
                EMAIL = userRequest.Email,
                CPF = userRequest.CPF,
                SENHA = userRequest.password,
                TELEFONE = userRequest.Phone,
                NASCIMENTO = userRequest.DateNasc,
                USUARIO_PERFIL = new List<USUARIO_PERFIL> {
                    new USUARIO_PERFIL {
                        ID_PERFIL = 1
                    }
                }
            };
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