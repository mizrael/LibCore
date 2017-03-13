using LibCore.CQRS.Validation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace LibCore.CQRS.Commands.Handlers
{
    public abstract class BaseCommandHandler<TCommand> : IAsyncNotificationHandler<TCommand>
        where TCommand : MediatR.INotification
    {
        private readonly IValidator<TCommand> _validator;

        protected BaseCommandHandler(IValidator<TCommand> validator)
        {
            _validator = validator;
        }

        public async Task Handle(TCommand command)
        {
            if (null == command)
                throw new ArgumentNullException(nameof(command));

            if (null != _validator)
            {
                var result = await _validator.ValidateAsync(command);

                if (null == result)
                    throw new ValidationException("command validation failed");

                if (!result.Success)
                    throw new ValidationException("command validation failed", result.Errors);
            }

            await RunCommand(command);
        }

        protected abstract Task RunCommand(TCommand command);
    }
}
