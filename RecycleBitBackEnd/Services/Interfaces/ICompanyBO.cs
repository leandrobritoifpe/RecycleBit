using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Models.Request;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     Class interface responsible per instance methods Company
    /// </summary>
    public interface ICompanyBO {

        #region Public Methods

        /// <summary>
        ///     Method interface responsible for creating or updating a Company
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CompanyDTO CreateCompany(CreateOrUpdateCompanyRequest request);

        /// <summary>
        ///     Method interface responsible for user login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        CompanyDTO Login(string email, string password);

        /// <summary>
        ///     Method responsible for retrieving all companies from the database
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        CompanyDTO GetCompanyByCnpj(string cnpj);

        /// <summary>
        ///     Method interface responsible for retrieving a company by email
        /// </summary>
        /// <returns></returns>
        List<CompanyDTO> GetAllCompanies();

        /// <summary>
        ///     Method interface responsible for deleting a company in the system by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        string DeleteCompany(string cnpj);

        /// <summary>
        ///     Method interface responsible for editing a company in the system
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        CompanyDTO EditCompany(string cnpj, CreateOrUpdateCompanyRequest request);

        #endregion Public Methods
    }
}