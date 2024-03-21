using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class RemoveRolesCommandValidator : AbstractValidator<RemoveRolesCommand>
    {
        public RemoveRolesCommandValidator() { 
            RuleFor(x => x.userName).NotEmpty();
            RuleFor(x => x.roles).NotEmpty();
        }
    }
}
