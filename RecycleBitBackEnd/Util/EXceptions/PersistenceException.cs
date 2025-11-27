using System;
using System.Runtime.Serialization;

namespace RecycleBitBackEnd.Util.Exceptions {

    /// <summary>
    /// Class Project Exception
    /// </summary>
    [Serializable]
    public class PersistenceException : Exception {

        /// <summary>
        /// Constructor Default
        /// </summary>
        public PersistenceException() {
        }

        /// <summary>
        /// protected constructor(Without this constructor, deserialization will fail)
        /// </summary>
        protected PersistenceException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

        /// <summary>
        /// Constructor Project Exception with parameter Exception Message.
        /// </summary>
        /// <param name="message">Exception Message</param>
        public PersistenceException(string message)
            : base(message) {
        }

        /// <summary>
        /// Constructor Project Exception with parameters Exception Messagem and Inner exception.
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="inner">Exception Innet</param>
        public PersistenceException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}