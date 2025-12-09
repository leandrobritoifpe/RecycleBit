using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Dao.Interfaces {

    public interface ICompanyDao {

        #region Public Methods

        /// <summary>
        ///     Method responsible for creating a new company in the database
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        COMPANY CreateCompany(COMPANY company);

        /// <summary>
        ///     Method responsible for retrieving all companies from the database
        /// </summary>
        /// <param name="cpnj"></param>
        /// <returns></returns>
        COMPANY GetCompanyByCpnj(string cpnj);

        /// <summary>
        ///     Method responsible for retrieving all companies from the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        COMPANY GetCompanyByEmail(string email);

        /// <summary>
        ///     Method responsible for retrieving all companies from the database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        COMPANY Login(string email, string password);

        /// <summary>
        ///     Method responsible for retrieving all companies from the database
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        COMPANY EditCompany(string cnpj, CreateOrUpdateCompanyRequest request);

        /// <summary>
        ///     Method interface responsible for retrieving all companies from the database
        /// </summary>
        /// <returns></returns>
        List<COMPANY> GetAllCompanies();

        /// <summary>
        ///     Method interface responsible for deleting a company in the system by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        void DeleteCompany(string cnpj);

        #endregion Public Methods
    }
}