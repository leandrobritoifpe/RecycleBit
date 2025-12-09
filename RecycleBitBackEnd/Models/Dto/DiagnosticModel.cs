namespace RecycleBitBackEnd.Models.Dto {

    /// <summary>
    /// Diagnostic Object
    /// </summary>
    public class DiagnosticModel {

        #region Attributes Properties

        /// <summary>
        /// Database connection status
        /// </summary>
        public bool DataBaseAccess { get; set; }

        /// <summary>
        /// KeyVault connection status
        /// </summary>
        public bool KeyVaultAccess { get; set; }

        #endregion Attributes Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiagnosticModel() {
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="DataBaseAccess"></param>
        public DiagnosticModel(bool DataBaseAccess) {
            this.DataBaseAccess = DataBaseAccess;
        }

        #endregion Public Methods
    }
}