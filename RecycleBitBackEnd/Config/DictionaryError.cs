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
        #endregion

        #region Attribute Template Logs
        /// <summary>
        ///     Attribute TEMPLATE_ERROR_LOGGER
        /// </summary>
        public static string TEMPLATE_ERROR_LOGGER = "DateTime: {0} |  ErrorMessage: {1} | StackTrace: {2}";

        /// <summary>
        ///     Attribute TEMPLATE_ERROR_ENTITY_LOGGER
        /// </summary>
        public static string TEMPLATE_ERROR_ENTITY_LOGGER = "DateTime: {0} | Propriedade: {1} | ErrorMessage: {2} | StackTrace: {3}";
        #endregion
    }
}