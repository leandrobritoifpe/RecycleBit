
using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Exceptions;
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

        private readonly IUsersBO usersBO;

        public UserController() {
        }

        /// <summary>
        /// Constructor for the UserController class that initializes the UsersBO service.
        /// </summary>
        /// <param name="usersBO"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserController(IUsersBO usersBO) {
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
        public HttpResponseMessage LoginUser([FromBody] LoginRequest login) {
            try {
                UserDTO response = usersBO.Login(login.Email, login.Password);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Method to create a new user in the system.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetUserById")]
        [ValidateModel]
        public HttpResponseMessage GetUserById(int userIdApplicant, int userIdSearch, string role) {
            try {
                if (userIdApplicant != userIdSearch && !role.Contains("Administrator"))
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                UserDTO response = usersBO.getUserById(userIdSearch);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Method to create a new user in the system.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllUsers")]
        [ValidateModel]
        public HttpResponseMessage GetAllUsers([FromBody] GetAllUserReques request) {
            try {
                if (!request.Role.Contains("Administrator"))
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                System.Collections.Generic.List<UserDTO> response = usersBO.GettAllUsers();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.GetBaseException().Message);
            }
        }

    }
}