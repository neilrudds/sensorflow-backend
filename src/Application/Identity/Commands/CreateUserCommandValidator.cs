using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() { 
            RuleFor(x => x.userName).MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.firstName).MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.lastName).MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.email).MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.tenantId).NotEmpty();
        }
    }
}