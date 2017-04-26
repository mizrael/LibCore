using System;

namespace LibCore.Web.ErrorHandling.Builders
{
    public interface IApiErrorInfoBuilder<in TEx>
        where TEx : Exception
    {
        ApiErrorInfo Build(TEx ex);
    }
}