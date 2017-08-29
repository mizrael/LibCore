using LibCore.Web.Exceptions;

namespace LibCore.Web.ErrorHandling.Builders
{
    public class ApiErrorInfoBuilder : IApiErrorInfoBuilder<ApiException>
    {
        public ApiErrorInfo Build(ApiException ex, ApiErroInfoBuilderOptions options)
        {
            var message = options.LoggingLevel == ApiErroInfoBuilderOptions.LoggingLevels.Verbose ? ex.ToString() : ex.Message;
            var result = new ApiErrorInfo(message, ex.Details);

            return result;
        }
    }
}