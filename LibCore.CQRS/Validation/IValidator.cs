using System.Threading.Tasks;

namespace LibCore.CQRS.Validation
{
    public interface IValidator<in TCommand>        
    {
        Task<ValidationResult> ValidateAsync(TCommand command);
    }
}
