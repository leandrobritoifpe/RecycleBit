using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Util.Exceptions;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace RecycleBitBackEnd.Dao {

    /// <summary>
    ///
    /// </summary>
    public class CollectionPointDaoImpl : ICollectionPointDao {

        #region Public Methods

        /// <summary>
        ///     Method
        /// </summary>
        /// <param name="collectionPoint"></param>
        /// <returns></returns>
        public COLLECTION_POINT CreateCollectionPoint(COLLECTION_POINT collectionPoint) {
            try {
                RecycleBitEntities context = new();
                context.COLLECTION_POINT.Add(collectionPoint);
                context.SaveChanges();
                COLLECTION_POINT pointCadaster = context.COLLECTION_POINT.Where(point => point.NAME == collectionPoint.NAME).FirstOrDefault();
                return pointCadaster;
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        /// <summary>
        ///     Method responsible for get collection point by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public COLLECTION_POINT GetCollectionPointById(int id) {
            RecycleBitEntities context = new();
            COLLECTION_POINT collectionPoint = context.COLLECTION_POINT.Where(point => point.POINT_ID == id && point.STATUS == true).FirstOrDefault();
            return collectionPoint;
        }

        /// <summary>
        ///     Method responsible for retrieving all collection points by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public List<COLLECTION_POINT> GetAllCollectionPointByCnpj(string cnpj) {
            RecycleBitEntities context = new();
            return context.COLLECTION_POINT.Where(point => point.STATUS == true && point.COMPANY_ID.Equals(cnpj)).ToList();
        }

        /// <summary>
        ///     Method responsible for editing a collection point in the system
        /// </summary>
        /// <param name="idPoint"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public COLLECTION_POINT EditCollectionPoint(int idPoint, CreateOrUpdateCollectionPoint request) {
            try {
                RecycleBitEntities context = new();
                COLLECTION_POINT pointEdit = context.COLLECTION_POINT.Where(point => point.POINT_ID == idPoint && point.STATUS == true).FirstOrDefault();
                if (pointEdit != null) {
                    pointEdit.NAME = request.Name;
                    pointEdit.DESCRIPTION = request.Description;
                    pointEdit.ADDRESS.LONGITUDE = (decimal)(request.Address.Longitude);
                    pointEdit.ADDRESS.LATITUDE = (decimal)(request.Address.Latitude);
                    pointEdit.ADDRESS.STATE_ABBR = request.Address.StateAbbr;
                    pointEdit.ADDRESS.STATE = request.Address.State;
                    pointEdit.ADDRESS.CITY = request.Address.City;
                    pointEdit.ADDRESS.NEIGHBORHOOD = request.Address.Neighborhood;
                    pointEdit.ADDRESS.STREET = request.Address.Street;
                    pointEdit.ADDRESS.NUMBER = request.Address.Number;
                    pointEdit.ADDRESS.STREET = request.Address.Street;
                    pointEdit.ADDRESS.ZIP_CODE = request.Address.ZipCode;
                    context.SaveChanges();
                }
                return pointEdit;
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        ///     Method responsible for deleting a company by its CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        public void DeleteCollectionPoint(int id) {
            try {
                RecycleBitEntities context = new();
                context.COLLECTION_POINT.Where(point => point.POINT_ID == id).FirstOrDefault().STATUS = false;
                context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        #endregion
    }
}