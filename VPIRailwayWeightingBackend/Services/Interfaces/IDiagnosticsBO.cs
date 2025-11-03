using RecycleBitBackEnd.Models.Dto;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     Class for interface of Diagnostics from solution
    /// </summary>
    public interface IDiagnosticsBO {

        /// <summary>
        ///     Method responsable per return informations from solution
        /// </summary>
        /// <returns>
        ///     Current Diagnostics
        /// </returns>
        DiagnosticModel GetDiagnosticsFromSolution();
    }
}