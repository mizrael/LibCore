using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibCore.Web.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, IEnumerable<Models.ApiError> errors) : base(message)
        {
            var errorsList = (errors ?? Enumerable.Empty<Models.ApiError>()).ToList();
            this.Details = new ReadOnlyCollection<Models.ApiError>(errorsList);
        }

        public readonly IReadOnlyCollection<Models.ApiError> Details;
    }
}
