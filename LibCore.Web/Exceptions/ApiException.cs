using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibCore.Web.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, IEnumerable<ErrorHandling.ApiError> errors) : base(message)
        {
            var errorsList = (errors ?? Enumerable.Empty<ErrorHandling.ApiError>()).ToList();
            this.Details = new ReadOnlyCollection<ErrorHandling.ApiError>(errorsList);
        }

        public readonly IReadOnlyCollection<ErrorHandling.ApiError> Details;
    }
}
