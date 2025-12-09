using RecycleBitBackEnd.Models.Dto;

namespace RecycleBitBackEnd.Services.Interfaces {

    /// <summary>
    ///     Class for interface of Diagnostics from solution
    /// </summary>
    public interface IDiagnosticsBO {

        #region Public Methods

        /// <summary>
        ///     Method responsible per return information from solution
        /// </summary>
        /// <returns>
        ///     Current Diagnostics
        /// </returns>
        DiagnosticModel GetDiagnosticsFromSolution();

        #endregion Public Methods
    }
}