using System;
using System.Net;
using System.Runtime.Serialization;

namespace RecycleBitBackEnd.Util.Exceptions {

    /// <summary>
    /// Class Project Exception
    /// </summary>
    [Serializable]
    public class ProjectException : Exception {
        public HttpStatusCode Status { get; set; }
        public string StackTrace { get; set; }

        /// <summary>
        /// Constructor Default
        /// </summary>
        public ProjectException() {
            Status = HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// protected constructor(Without this constructor, deserialization will fail)
        /// </summary>
        protected ProjectException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            Status = HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Constructor Project Exception with parameter Exception Message.
        /// </summary>
        /// <param name="message">Exception Message</param>
        public ProjectException(string message)
            : base(message) {
            Status = HttpStatusCode.InternalServerError;
        }

        public ProjectException(string message, HttpStatusCode status)
            : base(message) {
            Status = status;
        }

        /// <summary>
        /// Constructor Project Exception with parameters Exception Messagem and Inner exception.
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="inner">Exception Innet</param>
        public ProjectException(string message, Exception inner)
            : base(message, inner) {
            Status = HttpStatusCode.InternalServerError;
        }
    }
}