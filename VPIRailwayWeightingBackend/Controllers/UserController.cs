
using HarpiaCommon.Services.Interfaces;

using System;

using System.Web.Http;
using System.Web.Http.Cors;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Filters;
using System.Net.Http;
using VPIRailwayWeightingBackend.Models.Request;
using System.Net;
using HarpiaCommon.Models.Request;
using RecycleBitBackEnd.Util;


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
        public HttpResponseMessage CreateUser([FromBody] UserRequest user) {
            try {
                string response = usersBO.CreateUser(user);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (Exception e) {
                loggerBO.SetErrorLog(new NewLogErrorRequest((string)ApplicationParameters.Params.ApplicationName, DateTime.Now, e.GetBaseException().Message, string.Empty, this.GetMethodContext(), e, e.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.GetBaseException().Message);
            }
        }
    }
}