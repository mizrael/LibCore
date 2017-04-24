using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using LibCore.CQRS.Validation;
using LibCore.Web.Exceptions;

namespace LibCore.Web.Models
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

        #region Factory

        public static ApiErrorInfo Create<TEx>(TEx ex)
            where TEx : Exception
        {
            if (null == ex)
                throw new ArgumentNullException(nameof(ex));

            IEnumerable<ApiError> errors = null;

            var validEx = ex as ValidationException;
            if (null != validEx)
                errors = validEx.Errors.Select(err => new ApiError(err.Context, err.Message));

            var apiEx = ex as ApiException;
            if (null != apiEx)
                errors = apiEx.Details;

            var result = new ApiErrorInfo(ex.Message, errors);

            return result;
        }

        #endregion Factory
    }
}