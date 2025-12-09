
using log4net;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Enums;
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
    [RoutePrefix("api/point")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CollectionPointController : ApiController {

        #region Attributes Properties
        private readonly ICompanyBO companyBO;
        private readonly ILog Logger = LogManager.GetLogger(typeof(CompanyController));
        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor for the UserController class.
        /// </summary>
        public CollectionPointController() {
        }

        /// <summary>
        ///     Constructor for the UserController class that initializes the UsersBO service.
        /// </summary>
        /// <param name="usersBO"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CollectionPointController(ICompanyBO conpanyBO) {
            this.companyBO = conpanyBO ?? throw new ArgumentNullException("conpanyBO");
        }
        #endregion

        #region Methods Public

        /// <summary>
        ///     Create a new company in the system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateCompany")]
        [ValidateModel]
        public HttpResponseMessage CreateCompany([FromBody] CreateOrUpdateCompanyRequest request) {
            try {
                CompanyDTO response = companyBO.CreateCompany(request);
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
        /// <param name="cnpj"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCompanyByCnpj")]
        [ValidateModel]
        public HttpResponseMessage GetCompanyByCnpj(string cnpj) {
            try {
                CompanyDTO response = companyBO.GetCompanyByCpnj(cnpj);
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
        [ActionName("GetAllCompanies")]
        [ValidateModel]
        public HttpResponseMessage GetAllCompanies(Role role) {
            try {
                if (role != Role.Administrator)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, DictionaryError.ERROR_UNAUTHORIZED);
                var response = companyBO.GetAllCompanies();
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
        ///     Method Controller responsible for deleting a company in the system.
        /// </summary>
        /// <param name="cnpjApplicant"></param>
        /// <param name="cnpjDelete"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteCompany")]
        [ValidateModel]
        public HttpResponseMessage DeleteCompany(string cnpjApplicant, string cnpjDelete, Role role) {
            try {
                if (role != Role.Administrator && !cnpjApplicant.Equals(cnpjDelete))
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, DictionaryError.ERROR_UNAUTHORIZED);
                string response = companyBO.DeleteCompany(cnpjDelete);
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
        [HttpPut]
        [ActionName("EditUser")]
        [ValidateModel]
        public HttpResponseMessage EditUser([FromBody] EditCompanyRequest request) {
            try {
                if (request.Role != Role.Administrator && !request.cnpjApplicant.Equals(request.cnpjEdit))
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, DictionaryError.ERROR_UNAUTHORIZED);
                CompanyDTO response = companyBO.EditCompany(request.cnpjEdit, request.Request);
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