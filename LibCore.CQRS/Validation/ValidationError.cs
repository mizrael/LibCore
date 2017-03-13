using System;

namespace LibCore.CQRS.Validation
{

    public class ValidationError
    {
        public ValidationError(string context, string message)
        {
            if (string.IsNullOrWhiteSpace(context))
                throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            this.Message = message;
            this.Context = context;
        }

        public string Context { get; private set; }
        public string Message { get; private set; }
    }

}