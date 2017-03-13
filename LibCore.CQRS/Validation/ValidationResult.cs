using System.Collections.Generic;
using System.Linq;

namespace LibCore.CQRS.Validation
{
    public class ValidationResult
    {
        public ValidationResult(IEnumerable<ValidationError> errors) {            
            this.Errors = (errors ?? Enumerable.Empty<ValidationError>()).ToArray();
        }

        public ValidationError[] Errors { get; private set; }

        public bool Success
        {
            get { return !this.Errors.Any(); }
        }        
    }

}
