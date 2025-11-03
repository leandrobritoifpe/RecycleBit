using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace System {
    /// <summary>
    /// A Class to extend DateTime functionalities
    /// </summary>
    public static class ObjectExtension {

        /// <summary>
        /// Clone an object using JSON serialization
        /// </summary>
        /// <returns>Cloned object</returns>
        public static T DeepClone<T>(this T origem) {
            var json = JsonConvert.SerializeObject(origem);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Get the context of the method call including class name and method name.
        /// </summary>
        /// <returns>string</returns>
        public static string GetMethodContext(this object obj, [CallerMemberName] string methodName = "") {
            string className = obj.GetType().Name;
            return $"{className} {methodName}";
        }
    }
}