using System.Threading.Tasks;

namespace LibCore.CQRS.Validation
{
    public sealed class NullValidator<T> : IValidator<T>
    {
        public Task<ValidationResult> ValidateAsync(T value)
        {
            return Task.FromResult(new ValidationResult(null));
        }
    }
}
