using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Models.Request;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     Class interface responsible per instance methods Company
    /// </summary>
    public interface ICollectionPointBO {

        #region Public Methods
        /// <summary>
        ///     Method interface responsible for creating or updating a Company
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CollectionPointDTO CreateCollectionPoint(CreateOrUpdateCollectionPoint request);

        /// <summary>
        ///     Method interface responsible for get collection point by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CollectionPointDTO GetCollectionPointById(int id);

        /// <summary>
        ///     Method interface responsible for retrieving all collection points by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        List<CollectionPointDTO> GetAllCollectionPointByCnpj(string cnpj);

        /// <summary>
        ///    Method responsible for deleting a collection point by Id
        /// </summary>
        /// <param name="id"></param>
        string DeleteCollectionPoint(int id);

        /// <summary>
        ///     Method interface responsible for editing a company in the system
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        CollectionPointDTO EditCollectionPoint(int id, CreateOrUpdateCollectionPoint request);

        #endregion
    }
}