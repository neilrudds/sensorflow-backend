using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator() {
            RuleFor(x => x.userId).NotEmpty();
        }
    }
}
