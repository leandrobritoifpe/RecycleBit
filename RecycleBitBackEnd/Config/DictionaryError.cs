namespace RecycleBitBackEnd.Config {

    /// <summary>
    ///     Class to configure and parameterize the attributes, in order to avoid hard code.
    /// </summary>
    public static class DictionaryError {

        #region Attribute Messages Error

        /// <summary>
        ///     Attribute IS_VALUE_NOT_NULL
        /// </summary>
        public const string IS_VALUE_NOT_NULL = "O Valor me questão não pode ser Nulo!!";

        /// <summary>
        ///     Attribute ID_ROLE_NO_REFERENCES
        /// </summary>
        public const string ID_ROLE_NO_REFERENCES = "Id {0} no references one id existents in database solution";

        /// <summary>
        ///     Attribute CPF_EXIST_IN_DATABASE
        /// </summary>
        public const string CPF_EXIST_IN_DATABASE = "O CPF informado já foi cadastrado";

        /// <summary>
        ///     Attribute CNPJ_EXIST_IN_DATABASE
        /// </summary>
        public const string CNPJ_EXIST_IN_DATABASE = "O CNPJ informado já foi cadastrado";

        /// <summary>
        ///     Attribute EMAIL_EXIST_IN_DATABASE
        /// </summary>
        public const string EMAIL_EXIST_IN_DATABASE = "O CPF informado já foi cadastrado";

        /// <summary>
        ///     Attribute INVALID_EMAIL_OR_PASSWORD
        /// </summary>
        public static string INVALID_EMAIL_OR_PASSWORD = "E-mail ou senha inválidos";

        /// <summary>
        ///     Attribute ERROR_UNAUTHORIZED
        /// </summary>
        public static string ERROR_UNAUTHORIZED = "O perfil atual não tem os privilégios necessário para realizar essa ação, qualquer dúvida entre em contato com o Administrador";

        /// <summary>
        ///     Attrbute ERROR_NO_CONTENT
        /// </summary>
        public static string ERROR_NO_CONTENT = "Não foi encontrado nenhum resultado para a solicitação requisitada";

        #endregion Attribute Messages Error

        #region Attribute Template Logs

        /// <summary>
        ///     Attribute TEMPLATE_ERROR_LOGGER
        /// </summary>
        public static string TEMPLATE_ERROR_LOGGER = "DateTime: {0} |  ErrorMessage: {1} | StackTrace: {2}";

        /// <summary>
        ///     Attribute TEMPLATE_ERROR_ENTITY_LOGGER
        /// </summary>
        public static string TEMPLATE_ERROR_ENTITY_LOGGER = "DateTime: {0} | Propriedade: {1} | ErrorMessage: {2} | StackTrace: {3}";

        /// <summary>
        ///     Attribute ERROR_CREATE_COMPANY
        /// </summary>
        public static string ERROR_CREATE_COMPANY = "An unexpected error occurred while trying to create the company in the database.";

        /// <summary>
        ///     Attribute ERROR_CREATE_COLLECTION_POINT
        /// </summary>
        public static string ERROR_CREATE_COLLECTION_POINT = "An unexpected error occurred while trying to create the collection point in the database.";

        /// <summary>
        ///     Attribute COMPANY_NOT_FOUND
        /// </summary>
        public static string COMPANY_NOT_FOUND = "A Company {0} not found in database.";

        /// <summary>
        ///     Atrribute COLLETION_POINT__NOT_FOUND
        /// </summary>
        public static string COLLETION_POINT__NOT_FOUND = "A Collection Point id {0} not found in database.";

        #endregion Attribute Template Logs
    }
}