using System;

namespace LibCore.Web.ErrorHandling
{
    public interface IApiErrorInfoFactory
    {
        ApiErrorInfo Create<TEx>(TEx ex) where TEx : Exception;
    }
}