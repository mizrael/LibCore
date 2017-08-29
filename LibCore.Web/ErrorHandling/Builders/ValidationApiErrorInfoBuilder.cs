using LibCore.CQRS.Validation;
using System.Linq;

namespace LibCore.Web.ErrorHandling.Builders
{
    public class ValidationApiErrorInfoBuilder : IApiErrorInfoBuilder<ValidationException>
    {
        public ApiErrorInfo Build(ValidationException ex, ApiErroInfoBuilderOptions options)
        {
            var message = options.LoggingLevel == ApiErroInfoBuilderOptions.LoggingLevels.Verbose ? ex.ToString() : ex.Message;
            var errors = ex.Errors.Select(err => new ApiError(err.Context, err.Message));

            var result = new ApiErrorInfo(message, errors);

            return result;
        }
    }
}