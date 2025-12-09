using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Exceptions;
using System;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    ///     Class responsible per implement methods for interface ICompany BO
    /// </summary>
    public class CollectionPointBOImpl : ICollectionPointBO {

        #region Attribute Propeties

        private readonly ICollectionPointDao collectionDao;
        private readonly IAddressBO addressBo;

        #endregion Attribute Propeties

        #region Constructors

        public CollectionPointBOImpl() {
        }

        /// <summary>
        /// Constructor for the NewUserBOImpl class that initializes the logger and DAO.
        /// </summary>
        /// <param name="usersDao"></param>
        public CollectionPointBOImpl(ICollectionPointDao collectionDao, IAddressBO addressBo) {
            this.collectionDao = collectionDao ?? throw new ArgumentNullException("collectionDao");
            this.addressBo = addressBo ?? throw new ArgumentNullException("addressBo");
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        ///     Method responsible for creating collection point
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        public CollectionPointDTO CreateCollectionPoint(CreateOrUpdateCollectionPoint request) {
            ADDRESS Adrres = addressBo.SaveAddress(request.Address);

            COLLECTION_POINT collectionInsert = MappingDataCollectionPoint(request, Adrres.ADDRESS_ID);

            COLLECTION_POINT companyCreated = collectionDao.CreateCollectionPoint(collectionInsert);

            if (companyCreated == null)
                throw new ProjectException(DictionaryError.ERROR_CREATE_COLLECTION_POINT);

            return ConvertObjectCollectionPointDTO(companyCreated);
        }

        /// <summary>
        ///     Method responsible for get collection point by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        public CollectionPointDTO GetCollectionPointById(int id) {
            COLLECTION_POINT point = collectionDao.GetCollectionPointById(id);
            if (point == null)
                throw new ProjectException(String.Format(DictionaryError.COLLETION_POINT__NOT_FOUND, id));
            return ConvertObjectCollectionPointDTO(point);
        }

        /// <summary>
        ///  Method responsible for retrieving all collection points by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public List<CollectionPointDTO> GetAllCollectionPointByCnpj(string cnpj) {
            List<CollectionPointDTO> points = new();
            List<COLLECTION_POINT> pointListDatabase = collectionDao.GetAllCollectionPointByCnpj(cnpj);
            foreach (COLLECTION_POINT company in pointListDatabase) {
                try {
                    points.Add(ConvertObjectCollectionPointDTO(company));
                } catch (Exception ex) {
                    throw ex;
                }
            }
            return points;
        }

        /// <summary>
        ///     Method responsible for deleting a collection point by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteCollectionPoint(int id) {
            collectionDao.DeleteCollectionPoint(id);
            return DictionaryMessageView.DELETE_COMPANY_SUCESS;
        }

        /// <summary>
        ///     Method responsible for editing a company in the system
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public CollectionPointDTO EditCollectionPoint(int idPoint, CreateOrUpdateCollectionPoint request) {
            COLLECTION_POINT companyEdit = collectionDao.EditCollectionPoint(idPoint, request);
            return ConvertObjectCollectionPointDTO(companyEdit);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Method responsible for mapping data from CreateOrUpdateCollectionPoint to Collection Point model
        /// </summary>
        /// <param name="request"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        private COLLECTION_POINT MappingDataCollectionPoint(CreateOrUpdateCollectionPoint request, int addressId) {
            COLLECTION_POINT colletcionPoint = new() {
                NAME = request.Name,
                DESCRIPTION = request.Description,
                COMPANY_ID = request.CompanyCNPJ,
                STATUS = request.Status,
                ADDRESS_ID = addressId,
            };
            return colletcionPoint;
        }

        /// <summary>
        ///     Private method responsible for converting the COMPANY model to the CompanyDTO model
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private CollectionPointDTO ConvertObjectCollectionPointDTO(COLLECTION_POINT point) {
            CollectionPointDTO pointDto = new() {
                Name = point.NAME,
                Status = point.STATUS,
                Description = point.DESCRIPTION,
                CompanyCNPJ = point.COMPANY_ID,
                Address = new() {
                    Id = point.ADDRESS.ADDRESS_ID,
                    Street = point.ADDRESS.STREET,
                    Number = point.ADDRESS.NUMBER,
                    Neighborhood = point.ADDRESS.NEIGHBORHOOD,
                    City = point.ADDRESS.CITY,
                    State = point.ADDRESS.STATE,
                    ZipCode = point.ADDRESS.ZIP_CODE,
                    Longitude = (double)(point.ADDRESS.LONGITUDE ?? 0),
                    Latitude = (double)(point.ADDRESS.LATITUDE ?? 0)
                }
            };
            return pointDto;
        }

        #endregion Private Methods
    }
}