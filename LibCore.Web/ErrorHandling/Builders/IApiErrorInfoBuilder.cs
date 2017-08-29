using System;

namespace LibCore.Web.ErrorHandling.Builders
{
    public interface IApiErrorInfoBuilder<in TEx>
        where TEx : Exception
    {
        ApiErrorInfo Build(TEx ex, ApiErroInfoBuilderOptions options);
    }

    public class ApiErroInfoBuilderOptions
    {
        public ApiErroInfoBuilderOptions(LoggingLevels logLevel)
        {
            this.LoggingLevel = logLevel;
        }

        public LoggingLevels LoggingLevel { get; private set; }

        public enum LoggingLevels
        {
            Verbose = 0,
            Minimal
        }
    }
}