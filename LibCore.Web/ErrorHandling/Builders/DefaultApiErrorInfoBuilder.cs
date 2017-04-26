namespace LibCore.Web.ErrorHandling.Builders
{
    public class DefaultApiErrorInfoBuilder : IApiErrorInfoBuilder<System.Exception>
    {
        public ApiErrorInfo Build(System.Exception ex)
        {
            var result = new ApiErrorInfo(ex.Message);

            return result;
        }
    }
}