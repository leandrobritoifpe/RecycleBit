using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util;
using RecycleBitBackEnd.Util.Exceptions;
using System;
using System.Collections.Generic;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    ///     Class responsible per implement methods for interface ICompany BO
    /// </summary>
    public class CompanyBOImpl : ICompanyBO {

        #region Attribute Propeties
        private readonly ICompanyDao companyDao;
        private readonly IAddressBO addressBo;
        #endregion

        #region Constructors

        #endregion

        #region Public Methods
        /// <summary>
        ///     Method respons
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public CompanyDTO CreateCompany(CreateOrUpdateCompanyRequest request) {

            if (companyDao.GetCompanyByCpnj(request.CNPJ) != null)
                throw new ProjectException(DictionaryError.CNPJ_EXIST_IN_DATABASE);

            if (companyDao.GetCompanyByEmail(request.Email) != null)
                throw new ProjectException(DictionaryError.EMAIL_EXIST_IN_DATABASE);

            ADDRESS Adrres = addressBo.SaveAddress(request.Address);

            COMPANY userInsert = MappingDataCompany(request, Adrres.ADDRESS_ID);

            COMPANY companyCreated = companyDao.CreateCompany(userInsert);

            if (companyCreated == null)
                throw new ProjectException(DictionaryError.ERROR_CREATE_COMPANY);

            return ConvertObjectCompanyDTO(companyCreated);
        }

        /// <summary>
        ///     Method per login in the system
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CompanyDTO Login(string email, string password) {
            COMPANY company = companyDao.Login(email, password);
            if (company == null)
                return null;
            return ConvertObjectCompanyDTO(company);
        }

        /// <summary>
        ///     Method responsible for get company by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        /// <exception cref="ProjectException"></exception>
        public CompanyDTO GetCompanyByCpnj(string cnpj) {
            COMPANY company = companyDao.GetCompanyByCpnj(cnpj);
            if (company == null)
                throw new ProjectException(String.Format(DictionaryError.COMPANY_NOT_FOUND, cnpj));
            return ConvertObjectCompanyDTO(company);
        }

        /// <summary>
        ///     Method responsible for getting all companies in the system
        /// </summary>
        /// <returns></returns>
        public List<CompanyDTO> GetAllCompanies() {
            List<CompanyDTO> companies = new();
            List<COMPANY> companyList = companyDao.GetAllCompanies();
            foreach (COMPANY company in companyList) {
                try {
                    companies.Add(ConvertObjectCompanyDTO(company));
                } catch (Exception ex) {
                    throw;
                }
            }
            return companies;
        }

        /// <summary>
        ///     Method responsible for deleting a company in the system by CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <exception cref="NotImplementedException"></exception>
        public string DeleteCompany(string cnpj) {
            companyDao.DeleteCompany(cnpj);
            return DictionaryMessageView.DELETE_COMPANY_SUCESS;
        }

        /// <summary>
        ///     Method responsible for editing a company in the system
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public CompanyDTO EditCompany(string cnpj, CreateOrUpdateCompanyRequest request) {
            request.CNPJ = request.CNPJ.Replace(".", "").Replace("-", "");
            COMPANY companyEdit = companyDao.EditCompany(cnpj, request);
            return ConvertObjectCompanyDTO(companyEdit);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Private method responsible for mapping the data from the request to the COMPANY model
        /// </summary>
        /// <param name="request"></param>
        /// <param name="adressId"></param>
        /// <returns></returns>
        private COMPANY MappingDataCompany(CreateOrUpdateCompanyRequest request, int adressId) {
            COMPANY companyInsert = new() {
                CNPJ = request.CNPJ.Replace(".", "").Replace("-", ""),
                EMAIL = request.Email,
                PASSWORD = Encriptor.GenerateMD5(request.Password),
                CORPORATE_NAME = request.CorporateName,
                STATUS = request.Status,
                ADDRESS_ID = adressId,
                RESPONSIBLE = request.Responsible,
            };
            return companyInsert;
        }

        /// <summary>
        ///     Private method responsible for converting the COMPANY model to the CompanyDTO model
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        private CompanyDTO ConvertObjectCompanyDTO(COMPANY Company) {
            CompanyDTO companyDto = new() {
                CorporateName = Company.CORPORATE_NAME,
                Status = Company.STATUS,
                Email = Company.EMAIL,
                AddressId = Company.ADDRESS_ID,
                Address = new() {
                    Id = Company.ADDRESS.ADDRESS_ID,
                    Street = Company.ADDRESS.STREET,
                    Number = Company.ADDRESS.NUMBER,
                    Neighborhood = Company.ADDRESS.NEIGHBORHOOD,
                    City = Company.ADDRESS.CITY,
                    State = Company.ADDRESS.STATE,
                    ZipCode = Company.ADDRESS.ZIP_CODE,
                    Longitude = (double)(Company.ADDRESS.LONGITUDE ?? 0),
                    Latitude = (double)(Company.ADDRESS.LATITUDE ?? 0)
                }
            };
            return companyDto;
        }
        #endregion
    }
}