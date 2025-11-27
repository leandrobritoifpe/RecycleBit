namespace RecycleBitBackEnd.Config {

    /// <summary>
    ///     Class to configure and parameterize the attributes, in order to avoid hard code.
    /// </summary>
    public static class DictionaryError {

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
    }
}