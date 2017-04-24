using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LibCore.Web.Models;
using LibCore.CQRS.Validation;
using LibCore.Web.Exceptions;

namespace LibCore.Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = 500;

            if (context.Exception is System.ArgumentException ||
               context.Exception is System.ArgumentNullException ||
               context.Exception is System.ArgumentOutOfRangeException ||
               context.Exception is ApiException ||
               context.Exception is ValidationException)
                status = 400;

            var errorInfo = ApiErrorInfo.Create(context.Exception);
            context.Result = new ObjectResult(errorInfo)
            {
                StatusCode = status,
                DeclaredType = errorInfo.GetType()
            };
        }
    }
}