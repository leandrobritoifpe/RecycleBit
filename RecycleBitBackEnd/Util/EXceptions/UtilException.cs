using System;
using System.Runtime.Serialization;

namespace RecycleBitBackEnd.Util.Exceptions {

    /// <summary>
    /// Project exception class
    /// </summary>
    [Serializable]
    public class UtilException : Exception {

        /// <summary>
        /// Default constructor
        /// </summary>
        public UtilException() {
        }

        /// <summary>
        /// protected constructor(Without this constructor, deserialization will fail)
        /// </summary>
        protected UtilException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

        /// <summary>
        /// ProjectException constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public UtilException(string message)
            : base(message) {
        }

        /// <summary>
        /// ProjectException constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner Exception object</param>
        public UtilException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}