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
    public class ComapnyDaoImpl : ICompanyDao {

        #region Public Methods

        /// <summary>
        ///     Method
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public COMPANY CreateCompany(COMPANY company) {
            try {
                RecycleBitEntities context = new();
                context.COMPANY.Add(company);
                context.SaveChanges();

                COMPANY companyCadaster = context.COMPANY.Where(comp => comp.EMAIL == company.EMAIL && comp.CNPJ == company.CNPJ).FirstOrDefault();
                return companyCadaster;
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        /// <summary>
        ///     Method responsible for retrieving a company by its CNPJ
        /// </summary>
        /// <param name="cpnj"></param>
        /// <returns></returns>
        public COMPANY GetCompanyByCpnj(string cpnj) {
            RecycleBitEntities context = new();
            COMPANY conpany = context.COMPANY.Where(comp => comp.CNPJ == cpnj && comp.STATUS == true).FirstOrDefault();
            return conpany;
        }

        /// <summary>
        ///     Method responsible for retrieving a company by its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public COMPANY GetCompanyByEmail(string email) {
            RecycleBitEntities context = new();
            COMPANY companySearched = context.COMPANY.Where(comp => comp.EMAIL == email && comp.STATUS == true).FirstOrDefault();
            return companySearched;
        }

        /// <summary>
        ///     Method responsible for logging in a company
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public COMPANY Login(string email, string password) {
            RecycleBitEntities context = new();
            COMPANY conpearched = context.COMPANY.Where(comp => comp.EMAIL == email && comp.PASSWORD == password && comp.STATUS == true).FirstOrDefault();
            return conpearched;
        }

        /// <summary>
        ///     Method responsible for retrieving all companies from the database
        /// </summary>
        /// <returns></returns>
        public List<COMPANY> GetAllCompanys() {
            RecycleBitEntities context = new();
            return context.COMPANY.Where(comp => comp.STATUS == true).ToList();
        }

        /// <summary>
        ///     Method responsible for editing a company in the system
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public COMPANY EditCompany(string cnpj, CreateOrUpdateCompanyRequest request) {
            try {
                RecycleBitEntities context = new();
                COMPANY companyEdit = context.COMPANY.Where(comp => comp.CNPJ == cnpj).FirstOrDefault();
                if (companyEdit != null) {
                    companyEdit.CORPORATE_NAME = request.CorporateName;
                    companyEdit.RESPONSIBLE = request.Responsible;
                    companyEdit.STATUS = request.Status;
                    companyEdit.ADDRESS.LONGITUDE = (decimal)(request.Address.Longitude);
                    companyEdit.ADDRESS.LATITUDE = (decimal)(request.Address.Latitude);
                    companyEdit.ADDRESS.STATE_ABBR = request.Address.StateAbbr;
                    companyEdit.ADDRESS.STATE = request.Address.State;
                    companyEdit.ADDRESS.CITY = request.Address.City;
                    companyEdit.ADDRESS.NEIGHBORHOOD = request.Address.Neighborhood;
                    companyEdit.ADDRESS.STREET = request.Address.Street;
                    companyEdit.ADDRESS.NUMBER = request.Address.Number;
                    companyEdit.ADDRESS.STREET = request.Address.Street;
                    companyEdit.ADDRESS.ZIP_CODE = request.Address.ZipCode;
                    context.SaveChanges();
                }
                return companyEdit;
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        /// <summary>
        ///     Method interface responsible for retrieving all companies from the database
        /// </summary>
        /// <returns></returns>
        public List<COMPANY> GetAllCompanies() {
            RecycleBitEntities context = new();
            return context.COMPANY.Where(compn => compn.STATUS == true).ToList();
        }

        /// <summary>
        ///     Method responsible for deleting a company by its CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        public void DeleteCompany(string cnpj) {
            try {
                RecycleBitEntities context = new();
                context.COMPANY.Where(comp => comp.CNPJ.Equals(cnpj)).FirstOrDefault().STATUS = false;
                context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                EntityException.SetLoggerDbEntity(ex);
                throw ex;
            }
        }

        #endregion Public Methods
    }
}