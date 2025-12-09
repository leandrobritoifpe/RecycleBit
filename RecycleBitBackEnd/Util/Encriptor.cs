using System;
using System.Security.Cryptography;
using System.Text;

namespace RecycleBitBackEnd.Util {

    /// <summary>
    ///     Class responsible per encrypting and decrypting passwords
    /// </summary>
    public class Encriptor {

        #region Public Methods

        /// <summary>
        ///     Method responsible per encrypting a password using Base64
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string simpleEncriptBase64(string password) {
            Byte[] binary = Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(binary);
        }

        /// <summary>
        ///     Method responsible per decrypting a password using Base64
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string simpleDecriptBase64(string password) {
            Byte[] binary = Convert.FromBase64String(password);
            return Encoding.Default.GetString(binary);
        }

        /// <summary>
        ///     Method responsible for generating a MD5 hash from a password
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
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

        #endregion Public Methods
    }
}