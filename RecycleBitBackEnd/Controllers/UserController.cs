using HarpiaCommon.Exceptions;
using HarpiaCommon.Models.Request;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util;
using RecycleBitBackEnd.Util.Filters;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RecycleBitBackEnd.Controllers {

    /// <summary>
    /// Controller to configure the solutions APIs related to Configuration information
    /// </summary>
    [RoutePrefix("api/user")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController {
        private readonly IHarpiaLoggerBO loggerBO;
        private readonly IUsersBO usersBO;
        private readonly IPublicationBO auditBO;

        public UserController() {
        }

        /// <summary>
        /// Constructor for the UserController class that initializes the UsersBO service.
        /// </summary>
        /// <param name="loggerBO"></param>
        /// <param name="usersBO"></param>
        /// <param name="auditBO"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserController(IUsersBO usersBO, IPublicationBO auditBO) {
            this.usersBO = usersBO ?? throw new ArgumentNullException("usersBO");
        }

        /// <summary>
        /// Method to create a new user in the system.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("CreateUser")]
        [ValidateModel]
        public HttpResponseMessage CreateUser([FromBody] CreateUserRequest user) {
            try {
                string response = usersBO.CreateUser(user);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception e) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, e.GetBaseException().Message, string.Empty, this.GetMethodContext(), e, e.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Method to create a new user in the system.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("Login")]
        [ValidateModel]
        public HttpResponseMessage LoginUser([FromBody][ValidateEmail] string email, [FromBody][ValidatePassword] string password) {
            try {
                USER response = usersBO.Login(email, password);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception e) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, e.GetBaseException().Message, string.Empty, this.GetMethodContext(), e, e.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.GetBaseException().Message);
            }
        }
    }
}