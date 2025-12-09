using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Services.Interfaces;
using System;
using System.Linq;

namespace RecycleBitBackEnd.Services {

    /// <summary>
    /// Class Responsable to get solution diagnostic status
    /// </summary>
    public class DiagnosticsBOImpl : IDiagnosticsBO {

        #region Contructors

        /// <summary>
        ///     Method Constructor DiagnosticsBOImpl
        /// </summary>
        public DiagnosticsBOImpl() {
        }

        #endregion Contructors

        /// <summary>
        /// Get communication diagnostic that solution needs to be ready for use
        /// </summary>
        /// <returns></returns>
        public DiagnosticModel GetDiagnosticsFromSolution() {
            DiagnosticModel diagnosticModel = new DiagnosticModel {
                DataBaseAccess = ValidateAccessDatabase(),
            };
            return diagnosticModel;
        }

        #region Private Methods

        private bool ValidateAccessDatabase() {
            try {
                RecycleBitEntities context = new();
                int count = context.COLLECTION_POINT.Count();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        #endregion Private Methods
    }
}