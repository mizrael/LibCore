using LibCore.CQRS.Validation;
using LibCore.Web.ErrorHandling;
using LibCore.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibCore.Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IApiErrorInfoFactory _apiErrorFactory;

        public ExceptionFilter(IApiErrorInfoFactory factory)
        {
            _apiErrorFactory = factory ?? throw new System.ArgumentNullException(nameof(factory));
        }

        public void OnException(ExceptionContext context)
        {
            int status = ExtractHttpStatus(context);

            // had to cast to dynamic in order to avoid System.Exception to get passed to the Create method
            // see http://stackoverflow.com/questions/43640057/generic-method-called-with-base-type for details
            var errorInfo = _apiErrorFactory.Create((dynamic)context.Exception);

            context.Result = new ObjectResult(errorInfo)
            {
                StatusCode = status,
                DeclaredType = errorInfo.GetType()
            };
        }

        private static int ExtractHttpStatus(ExceptionContext context)
        {
            var status = 500;

            if (context.Exception is ValidationException ||
                context.Exception is ApiException)
                status = 400;

            return status;
        }
    }
}