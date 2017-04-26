using LibCore.CQRS.Validation;
using System.Linq;

namespace LibCore.Web.ErrorHandling.Builders
{
    public class ValidationApiErrorInfoBuilder : IApiErrorInfoBuilder<ValidationException>
    {
        public ApiErrorInfo Build(ValidationException ex)
        {
            var errors = ex.Errors.Select(err => new ApiError(err.Context, err.Message));

            var result = new ApiErrorInfo(ex.Message, errors);

            return result;
        }
    }
}