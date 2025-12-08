using System;
using System.Text;

namespace RecycleBitBackEnd.Util {

    /// <summary>
    ///     Class responsible per encrypting and decrypting passwords
    /// </summary>
    public class Encriptor {

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
    }
}