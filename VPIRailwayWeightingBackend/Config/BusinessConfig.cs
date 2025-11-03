using System;

namespace RecycleBitBackEnd.Config {

    /// <summary>
    /// Class to configure and parameterize the attributes, in order to avoid hard code.
    /// </summary>
    public static class BusinessConfig {


        /// <summary>
        /// The media type header
        /// </summary>
        public static readonly string MEDIA_TYPE_HEADER = "application/json";

        /// <summary>
        /// The media type header
        /// </summary>
        public static readonly string GPVM_DESTINATION_CODE = "GPV-M";
        /// <summary>
        /// The media type header
        /// </summary>
        public static readonly string MRS_DESTINATION_CODE = "MRS";

        /// <summary>
        /// The datetime ISO8601 format
        /// </summary>
        public static readonly string DATETIME_ISO8601_FORMAT_BRAZIL = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'-03:00'";

        /// <summary>
        /// The datetime ISO8601 format
        /// </summary>
        public static readonly string DATETIME_ISO8601_FORMAT_UTC = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";

        /// <summary>
        /// The datetime ISO8601 format
        /// </summary>
        public static readonly string DATETIME_ISO8601_FORMAT = "yyyy'-'MM'-'dd'T'HH':'mm':'sszzz";

        /// <summary>
        /// The regex to get parameters
        /// </summary>
        public static readonly string REGEX_TO_GET_PARAMETERS = @"(?:[^,()]+((?:\((>[^()]+|\((<open>)|\)(<-open>))*((open)(?!))\)))*)+";

        /// <summary>
        /// Single quotes
        /// </summary>
        public static readonly string SINGLE_QUOTES = "'";

        /// <summary>
        /// The IP21 date format
        /// </summary>
        public static readonly string IP21_DATE_FORMAT = "dd-MMM-yy HH:mm:ss.f";

        /// <summary>
        /// Comma
        /// </summary>
        public static readonly char COMMA = ',';

        /// <summary>
        /// Dot
        /// </summary>
        public static readonly char DOT = '.';

        /// <summary>
        /// Semicolon
        /// </summary>
        public static readonly char SEMICOLON = ';';

        /// <summary>
        /// The en-US Culture Info string
        /// </summary>
        public static readonly string CULTURE_INFO_EN_US = "en-US";

        /// <summary>
        /// String to identify the car type that starts with 'GD'
        /// </summary>
        public static readonly string CAR_TYPE_GD = "GD";

        /// <summary>
        /// Default user type
        /// </summary>
        public static readonly string DEFAULT_USER_TYPE = "UNR";

        /// <summary>
        /// Normative user role type
        /// </summary>
        public static readonly string NORMATIVE_ROLE = "NORMATIVE";

        /// <summary>
        /// Operational user role type
        /// </summary>
        public static readonly string OPERATIONAL_ROLE = "OPERATIONAL";

        /// <summary>
        /// XML Data source config string
        /// </summary>
        public static readonly string XML_DATA_SOURCE = "XML";

        /// <summary>
        /// Default OU Date Format string
        /// </summary>
        public static readonly string DEFAULT_OU_DATE_FORMAT = "DD/MM/YY HH:MI:SS:T";

        /// <summary>
        /// Date format string used in the GetValidCompValues method
        /// </summary>
        public static readonly string VALID_COMP_VALUES_DATE_FORMAT = "dd/MM/yy HH:mm:ss";

        /// <summary>
        /// The IAM Identity header
        /// </summary>
        public static readonly string IAM_IDENTITY_HEADER = "IAMIdentity";

        /// <summary>
        /// Default group
        /// </summary>
        public static readonly string DEFAULT_GROUP = "DefaultGroup";

        /// <summary>
        /// Trigger
        /// </summary>
        public static readonly string TRIGGER = "Trigger";

        /// <summary>
        /// Default axis number
        /// </summary>
        public static readonly int AXIS_NUMBER = 1;

        /// <summary>
        /// Default car type value
        /// </summary>
        public static readonly int CAR_TYPE = 1;

        /// <summary>
        /// Max car number
        /// </summary>
        public static readonly int MAX_CAR_NUMBER = 1000;

        /// <summary>
        /// Min car number
        /// </summary>
        public static readonly int MIN_CAR_NUMBER = 0;

        /// <summary>
        /// Date format used for generating the message ID
        /// </summary>
        public static readonly string MESSAGE_ID_DATE_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// Data insertion was successful
        /// </summary>
        public static readonly string INSERTION_SUCCESSFUL = "Dados gravados com sucesso.";

        /// <summary>
        ///
        /// </summary>
        public static readonly TimeZoneInfo BRAZIL_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        /// <summary>
        /// The datetime ISO8601 format XML send MRS
        /// </summary>
        public static readonly string DATETIME_ISO8601_FORMAT_BRAZIL_MRS = "yyyy-MM-ddTHH:mm:ss.ff";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string RESPONSE_CUSTOM_MRS_GPVM = "GPVM: {0} | MRS: {1}";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string RESPONSE_MESSAGE_CUSTOM_SUCESS = "Wheighing send to MRS sucess";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string SITE_NAME_FLOW_SUFIX = "Fluxo";

        /// <summary>
        /// Pipoint value Status Good
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_BAD = "Bad";

        /// <summary>
        /// Pipoint value Status Good
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_BAD_ANNOTATED = "Bad, Annotated";

        /// <summary>
        /// Pipoint value Status Good
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_BAD_SUBSTITUTE = "BadSubstituteValue";

        /// <summary>
        /// Pipoint value Status Good
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_BAD_SUBSTITUTE_ANNOTATED = "BadSubstituteValue, Annotated";        

        /// <summary>
        /// Pipoint value Status Good
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_GOOD = "Good";

        /// <summary>
        /// Pipoint value Status Good Annotated
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_GOOD_ANNOTATED = "Good, Annotated";

        /// <summary>
        /// Pipoint value Status Questionable
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_QUESTIONABLE = "Questionable";

        /// <summary>
        /// Pipoint value Status Questionable Annotated
        /// </summary>
        public static readonly string PIPOINT_VALUE_STATUS_QUESTIONABLE_ANNOTADED = "Questionable, Annotated";

        
    }
}