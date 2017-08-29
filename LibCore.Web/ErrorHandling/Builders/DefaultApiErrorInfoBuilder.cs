namespace LibCore.Web.ErrorHandling.Builders
{
    public class DefaultApiErrorInfoBuilder<TEx> : IApiErrorInfoBuilder<TEx> where TEx : System.Exception
    {
        public ApiErrorInfo Build(TEx ex, ApiErroInfoBuilderOptions options)
        {
            var message = options.LoggingLevel == ApiErroInfoBuilderOptions.LoggingLevels.Verbose ? ex.ToString() : ex.Message;
            var result = new ApiErrorInfo(message);

            return result;
        }
    }
}