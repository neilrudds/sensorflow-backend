using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator() { 
            RuleFor(x => x.userName).NotEmpty();
        }
    }
}
