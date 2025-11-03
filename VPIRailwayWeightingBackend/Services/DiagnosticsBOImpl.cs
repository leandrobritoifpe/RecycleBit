using System;
using System.Collections.Generic;
using System.Linq;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Services.Interfaces;

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
                //RailwayWeighingEntities context = new RailwayWeighingEntities();
                //return context.DUABT_LOT.OrderByDescending(tbLot => tbLot.DUAB_LOT_ID).Take(1).ToList().Count >= 0;
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}