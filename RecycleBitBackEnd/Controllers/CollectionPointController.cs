using log4net;
using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Exceptions;
using RecycleBitBackEnd.Util.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        private readonly ICollectionPointBO collectionPointBo;
        private readonly ICompanyBO companyBo;
        private readonly ILog Logger = LogManager.GetLogger(typeof(CompanyController));

        #endregion Attributes Properties

        #region Constructors

        /// <summary>
        ///     Constructor for the UserController class.
        /// </summary>
        public CollectionPointController() {
        }

        /// <summary>
        ///     Constructor for the UserController class with dependency injection.
        /// </summary>
        /// <param name="collectionPointBo"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CollectionPointController(ICollectionPointBO collectionPointBo, ICompanyBO companyBo) {
            this.collectionPointBo = collectionPointBo ?? throw new ArgumentNullException("collectionPointBo");
            this.companyBo = companyBo ?? throw new ArgumentNullException("companyBo");
        }

        #endregion Constructors

        #region Methods Public

        /// <summary>
        ///     Create a new company in the system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateCollectionPoint")]
        [ValidateModel]
        public HttpResponseMessage CreateCollectionPoint([FromBody] CreateOrUpdateCollectionPoint request) {
            try {
                CollectionPointDTO response = collectionPointBo.CreateCollectionPoint(request);
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
        ///     Mtethod responsible for get collection point by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCollectionPointById")]
        [ValidateModel]
        public HttpResponseMessage GetCollectionPointById(int id) {
            try {
                CollectionPointDTO response = collectionPointBo.GetCollectionPointById(id);
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
        [ActionName("GetAllCollectionPointByCnpj")]
        [ValidateModel]
        public HttpResponseMessage GetAllCollectionPointByCnpj(string cnpj) {
            try {
                List<CollectionPointDTO> response = collectionPointBo.GetAllCollectionPointByCnpj(cnpj);
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
        /// Method responsible for deleting a collection point by Id
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="idPoint"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteCollectionPoint")]
        [ValidateModel]
        public HttpResponseMessage DeleteCollectionPoint([ValidateCNPJ] string cnpj, [Required] int idPoint) {
            try {
                CompanyDTO company = companyBo.GetCompanyByCnpj(cnpj);
                if (company == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, String.Format(DictionaryError.COMPANY_NOT_FOUND, cnpj));
                if ((company.CollectionPoints != null && company.CollectionPoints.Where(p => p.Id == idPoint).Any()) || company.CollectionPoints == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, String.Format(DictionaryError.COLLETION_POINT__NOT_FOUND, idPoint));
                string response = collectionPointBo.DeleteCollectionPoint(idPoint);
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
        [HttpPost]
        [ActionName("EditCollectionPoint")]
        [ValidateModel]
        public HttpResponseMessage EditCollectionPoint([FromBody] EditColletionPointRequest request) {
            try {
                CompanyDTO company = companyBo.GetCompanyByCnpj(request.CompanyCNPJ);
                if (company == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, String.Format(DictionaryError.COMPANY_NOT_FOUND, request.CompanyCNPJ));
                if ((company.CollectionPoints != null && company.CollectionPoints.Where(p => p.Id == request.IdPoint).Any()) || company.CollectionPoints == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, String.Format(DictionaryError.COLLETION_POINT__NOT_FOUND, request.IdPoint));
                CollectionPointDTO response = collectionPointBo.EditCollectionPoint(request.IdPoint, request.Request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            } catch (ProjectException projEx) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), projEx.Message, projEx.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, projEx.Message);
            } catch (Exception ex) {
                Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_LOGGER, DateTime.Now.ToString(BusinessConfig.DEFAULT_OU_DATE_FORMAT), ex.Message, ex.StackTrace));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }

        #endregion Methods Public
    }
}