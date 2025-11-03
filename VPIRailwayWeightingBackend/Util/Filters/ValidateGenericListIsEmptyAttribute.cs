using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    /// Implementation to validate if List attribute is Empty
    /// </summary>
    public class ValidateGenericListIsEmptyAttribute<T> : ValidationAttribute {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            List<T> list = (List<T>)value;

            if (list.Count == 0)
                return false;

            return true;
        }
    }
}