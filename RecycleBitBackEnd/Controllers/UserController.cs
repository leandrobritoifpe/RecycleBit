
using log4net;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.models.dto;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Enums;
using RecycleBitBackEnd.Util.Exceptions;
using RecycleBitBackEnd.Util.Filters;
using System;
using System.Collections.Generic;
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

        #region Attributes Properties
        private readonly IUsersBO usersBO;
        private readonly ILog Logger = LogManager.GetLogger(typeof(UserController));
        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor for the UserController class.
        /// </summary>
        public UserController() {
        }

        /// <summary>
        ///     Constructor for the UserController class that initializes the UsersBO service.
        /// </summary>
        /// <param name="usersBO"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserController(IUsersBO usersBO) {
            this.usersBO = usersBO ?? throw new ArgumentNullException("usersBO");
        }
        #endregion

        #region Methods Public

        /// <summary>
        ///     Method to create a new user in the system.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateUser")]
        [ValidateModel]
        public HttpResponseMessage CreateUser([FromBody] CreateOrUpdateUserRequest user) {
            try {
                UserDTO response = usersBO.CreateUser(user);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), projEx.Message, projEx.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception ex) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), ex.Message, ex.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        ///     Method to login a user in the system.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        //[AcceptVerbs("POST")]
        [HttpPost]
        [ActionName("Login")]
        [ValidateModel]
        public HttpResponseMessage LoginUser([FromBody] LoginRequest login) {
            try {
                UserDTO response = usersBO.Login(login.Email, login.Password);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), projEx.Message, projEx.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception ex) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), ex.Message, ex.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        ///     Method to get a user by id in the system.
        /// </summary>
        /// <param name="userIdApplicant"></param>
        /// <param name="userIdSearch"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserById")]
        [ValidateModel]
        public HttpResponseMessage GetUserById(int userIdApplicant, int userIdSearch, Role role) {
            try {
                if (userIdApplicant != userIdSearch && role != Role.Administrator)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, DictionaryError.ERROR_UNAUTHORIZED);
                UserDTO response = usersBO.GetUserById(userIdSearch);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), projEx.Message, projEx.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception ex) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), ex.Message, ex.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        ///     Method responsible for getting all users in the system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAllUsers")]
        [ValidateModel]
        public HttpResponseMessage GetAllUsers([FromBody] GetAllUserReques request) {
            try {
                if (request.Role != Role.Administrator)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, DictionaryError.ERROR_UNAUTHORIZED);
                List<UserDTO> response = usersBO.GetAllUsers();
                if (response == null || response.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.NoContent, DictionaryError.ERROR_NO_CONTENT);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), projEx.Message, projEx.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception ex) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), ex.Message, ex.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        ///     Method responsible performing the deletion of a user in the system.
        /// </summary>
        /// <param name="userIdApplicant"></param>
        /// <param name="userIdDelete"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteUser")]
        [ValidateModel]
        public HttpResponseMessage DeleteUser(int userIdApplicant, int userIdDelete, Role role) {
            try {
                if (role != Role.Administrator && userIdApplicant != userIdDelete)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, DictionaryError.ERROR_UNAUTHORIZED);
                string response = usersBO.DeleteUser(userIdDelete);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), projEx.Message, projEx.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception ex) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), ex.Message, ex.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        ///     Method responsible performing the deletion of a user in the system.
        /// </summary>
        /// <param name="userIdApplicant"></param>
        /// <param name="userIdEdit"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("EditUser")]
        [ValidateModel]
        public HttpResponseMessage EditUser([FromBody] EditUserRequest request) {
            try {
                if (request.Role != Role.Administrator && request.UserIdApplicant != request.UserIdEdit)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, DictionaryError.ERROR_UNAUTHORIZED);
                UserDTO response = usersBO.EditUser(request.UserIdEdit, request.UserRequest);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), projEx.Message, projEx.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception ex) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), ex.Message, ex.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }
        #endregion
    }
}