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

        public DiagnosticsBOImpl() {
        }

        /// <summary>
        /// Get comunication diagnostic that solution needs to be ready for use
        /// </summary>
        /// <returns></returns>
        public DiagnosticModel GetDiagnosticsFromSolution() {
            DiagnosticModel diagnosticModel = new DiagnosticModel {
                DataBaseAccess = ValidateAccessDatabase(),
            };
            return diagnosticModel;
        }

        private bool ValidateAccessDatabase() {
            try {
                RecycleBitEntities context = new();
                int count = context.COLLECTION_POINT.Count();
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}