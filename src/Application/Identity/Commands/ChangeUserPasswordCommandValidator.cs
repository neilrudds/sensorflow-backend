using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator() { 
            RuleFor(x => x.userId).NotEmpty();
            RuleFor(x => x.oldPassword).NotEmpty();
            RuleFor(x => x.newPassword).NotEmpty();
        }
    }
}
