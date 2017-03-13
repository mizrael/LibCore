using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibCore.CQRS.Validation
{
    public abstract class Validator<TCommand> : IValidator<TCommand>
    {
        private readonly List<ValidationError> _errors;

        protected Validator()
        {
            _errors = new List<ValidationError>();
        }

        public async Task<ValidationResult> ValidateAsync(TCommand command)
        {
            _errors.Clear();

            if (null == command)
            {
                AddError(new ValidationError("command", "command cannot be null"));
            }
            else
            {
                await this.RunAsync(command);
            }

            var errors = _errors.Where(e => null != e).ToArray();
            var result = new ValidationResult(errors);

            return result;
        }

        protected void AddError(string field, string text, params object[] args)
        {
            var message = string.Format(text, args);
            this.AddError(new ValidationError(field, message));
        }

        protected void AddError(ValidationError error)
        {
            _errors.Add(error);
        }

        protected abstract Task RunAsync(TCommand command);
    }
}
