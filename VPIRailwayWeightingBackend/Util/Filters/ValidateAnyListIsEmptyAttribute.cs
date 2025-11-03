using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    /// Implementation to validate if List attribute is Empty
    /// </summary>
    public class ValidateAnyListIsEmptyAttribute : ValidationAttribute {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            IList list = value as IList;

            if (list.Count == 0)
                return false;

            return true;
        }
    }
}