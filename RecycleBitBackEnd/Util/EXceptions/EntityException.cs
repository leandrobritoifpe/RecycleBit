using log4net;
using RecycleBitBackEnd.Config;
using System;
using System.Data.Entity.Validation;

namespace RecycleBitBackEnd.Util.Exceptions {

    /// <summary>
    ///     Class responsible per handling Entity Framework exceptions.
    /// </summary>
    public static class EntityException {

        /// <summary>
        ///     Method responsible per logging Entity Framework validation errors.
        /// </summary>
        /// <param name="ex"></param>
        public static void SetLoggerDbEntity(DbEntityValidationException ex) {
            ILog Logger = LogManager.GetLogger(typeof(EntityException));
            foreach (DbEntityValidationResult validationErrors in ex.EntityValidationErrors) {
                foreach (DbValidationError validationError in validationErrors.ValidationErrors) {
                    Logger.Error(String.Format(DictionaryError.TEMPLATE_ERROR_ENTITY_LOGGER, DateTime.Now, validationError.PropertyName, validationError.ErrorMessage, ex.StackTrace));
                }
            }
        }
    }
}