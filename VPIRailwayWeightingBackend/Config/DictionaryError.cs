namespace RecycleBitBackEnd.Config {

    /// <summary>
    ///     Class to configure and parameterize the attributes, in order to avoid hard code.
    /// </summary>
    public static class DictionaryError {

        /// <summary>
        /// PiPoint not found.
        /// </summary>
        public static readonly string PIPOINT_NOT_FOUND = "PiPoint not found.";

        /// <summary>
        /// Message error when the request is not valid
        /// </summary>
        public static readonly string BAD_REQUEST = "Bad request.";

        /// <summary>
        /// Invalid parameters
        /// </summary>
        public static readonly string INVALID_PARAMETERS = "Invalid parameters.";

        /// <summary>
        /// Bad return on Simm Soft submission
        /// </summary>
        public static readonly string BAD_RETURN_SIMM_SOFT_SUBMISSION = "Bad return on Simm Soft submission.";

        /// <summary>
        /// Error when sending composition to Simm Soft
        /// </summary>
        public static readonly string ERROR_SENDING_COMPOSITION_TO_SIMM_SOFT = "Erro no envio da composição para o Simmsoft";

        /// <summary>
        /// Invalid value for the "Regra Habilitada" parameter
        /// </summary>
        public static readonly string INVALID_RULE_ENABLED_PARAMETER = "Valor para o parâmetro \"Regra Habilitada\" inválido. ";

        /// <summary>
        /// Invalid value for the "Regra Habilitada" parameter
        /// </summary>
        public static readonly string INVALID_MAX_CAR_NUMBER_PARAMETER = " Valor Inválido para o número de carros na regra (deve ser maior do que zero). ";

        /// <summary>
        /// Invalid value for the "Regra Habilitada" parameter
        /// </summary>
        public static readonly string INVALID_GROSS_WEIGHT_PARAMETER = " Valor Inválido para o peso bruto (deve ser maior do que zero). ";

        /// <summary>
        /// Error during tolerance rule creation
        /// </summary>
        public static readonly string TOLERANCE_RULE_CREATION_ERROR = "Erro durante criação da regra de tolerância: ";

        /// <summary>
        /// Tolerance rule not found
        /// </summary>
        public static readonly string TOLERANCE_RULE_NOT_FOUND = "A regra não foi encontrada";

        /// <summary>
        /// No valid weighings found
        /// </summary>
        public static readonly string NO_VALID_WEIGHINGS = "No valid weighings found for the given parameters.";

        /// <summary>
        /// Error when executing job
        /// </summary>
        public static readonly string ERROR_JOB_EXECUTION = "Error in job execution. Job: '{0}' / Datetime Start: '{1}'";

        /// <summary>
        /// Invalid axis value
        /// </summary>
        public static readonly string INVALID_AXIS_STRING_FORMAT = "The axis value {0} is invalid.";

        /// <summary>
        /// Bad return on TIBCO submission
        /// </summary>
        public static readonly string BAD_RETURN_API_SUBMISSION = "Bad return on API submission.";

        /// <summary>
        /// Empty sync response from TIBCO
        /// </summary>
        public static readonly string SYNC_EMPTY_TIBCO_RESPONSE = "A comunicação é síncrona mas o objeto de resposta da integração é nulo ou vazio.";

        /// <summary>
        /// Invalid status from TIBCO response
        /// </summary>
        public static readonly string INVALID_TIBCO_STATUS_RESPONSE = "Não foi retornado valor válido em {0}.";

        /// <summary>
        /// An error occured when reading logs from VPI Logger
        /// </summary>
        public static readonly string ERROR_READ_VPILOGGER = "An error occured when reading logs from VPI Logger";

        /// <summary>
        /// Bad return on TIBCO submission
        /// </summary>
        public static readonly string ERROR_SEND_DATA_MRS = "An error occurred when trying to send data to MRS";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string MESSAGE_CUSTOM_MRS = "{0} | Message: {1} | TypeError: {2}";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string MESSAGE_ERROR_SEND_MRS = "Aconteceu um erro inesperado ao tentar se conectar com o serviço do MRS, o mesmo returno status de envio de mensagem como FALSE. Entre em contato com o administrador";

    }
}