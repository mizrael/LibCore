using LibCore.Web.Exceptions;

namespace LibCore.Web.ErrorHandling.Builders
{
    public class ApiErrorInfoBuilder : IApiErrorInfoBuilder<ApiException>
    {
        public ApiErrorInfo Build(ApiException ex)
        {
            var result = new ApiErrorInfo(ex.Message, ex.Details);

            return result;
        }
    }
}