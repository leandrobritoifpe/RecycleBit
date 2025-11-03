using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    /// Implementation to validate if List attribute is Empty
    /// </summary>
    public class ValidateListIsEmptyAttribute : ValidationAttribute {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public override bool IsValid(object value) {
            if(value == null)
                return false;

            List<string> list = (List<string>) value;

            if(list.Count == 0)
                return false;

            return true;
        }
    }
}