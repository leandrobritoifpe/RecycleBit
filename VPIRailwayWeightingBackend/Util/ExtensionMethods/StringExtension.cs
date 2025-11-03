using System.Text;

namespace System {
    /// <summary>
    /// A Class to extend String functionalities
    /// </summary>
    public static class StringExtension {

        /// <summary>
        /// Encode a string to a 64 encoded string
        /// </summary>
        /// <returns>Encoded string</returns>
        public static string EncodeBase64(this string value) {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decode a 64 encoded string
        /// </summary>
        /// <returns>decoded string</returns>
        public static string DecodeBase64(this string value) {
            byte[] bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}