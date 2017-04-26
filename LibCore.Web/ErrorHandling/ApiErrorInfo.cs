using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibCore.Web.ErrorHandling
{
    public class ApiErrorInfo
    {
        public ApiErrorInfo(string message) : this(message, null)
        {
        }

        public ApiErrorInfo(string message, IEnumerable<ApiError> errors)
        {
            this.Message = message ?? string.Empty;

            this.Details = new ReadOnlyCollection<ApiError>((errors ?? Enumerable.Empty<ApiError>()).ToList());
        }

        public IReadOnlyCollection<ApiError> Details { get; private set; }

        public string Message { get; private set; }
       
    }
}