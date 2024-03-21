using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
    {
        public DeactivateUserCommandValidator() {
            RuleFor(x => x.userName).NotEmpty();
        }
    }
}
