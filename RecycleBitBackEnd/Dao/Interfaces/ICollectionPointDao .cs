using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Dao.Interfaces {

    /// <summary>
    /// 'Classe interface responsible per instance methods Collection Point
    /// </summary>
    public interface ICollectionPointDao {

        #region Public Methods

        /// <summary>
        ///     method responsible for creating collection point
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        COLLECTION_POINT CreateCollectionPoint(COLLECTION_POINT company);

        /// <summary>
        ///     Method responsible for get collection point by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        COLLECTION_POINT GetCollectionPointById(int id);

        /// <summary>
        ///     Collection point editing method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        COLLECTION_POINT EditCollectionPoint(int id, CreateOrUpdateCollectionPoint request);

        /// <summary>
        ///  Method interface responsible for retrieving all collection points by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        List<COLLECTION_POINT> GetAllCollectionPointByCnpj(string cnpj);

        /// <summary>
        ///     Method interface responsible per deleting a collection point by Id
        /// </summary>
        /// <param name="id"></param>
        void DeleteCollectionPoint(int id);

        #endregion Public Methods
    }
}