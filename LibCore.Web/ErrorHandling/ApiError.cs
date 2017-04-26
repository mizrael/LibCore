using System;

namespace LibCore.Web.ErrorHandling
{
    public class ApiError
    {
        public ApiError(string context, string message)
        {
            if (string.IsNullOrWhiteSpace(context))
                throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));
            this.Context = context;
            this.Message = message;
        }

        public string Context { get; private set; }
        public string Message { get; private set; }
    }
}