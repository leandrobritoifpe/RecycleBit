namespace RecycleBitBackEnd.Models.Dto {

    /// <summary>
    /// Dignostic Object
    /// </summary>
    public class DiagnosticModel {

        /// <summary>
        /// Database connection status
        /// </summary>
        public bool DataBaseAccess { get; set; }

        /// <summary>
        /// KeyVault connection status
        /// </summary>
        public bool KeyVaultAccess { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiagnosticModel() {
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="DataBaseAccess"></param>
        public DiagnosticModel( bool DataBaseAccess) {
            this.DataBaseAccess = DataBaseAccess;
        }
    }
}