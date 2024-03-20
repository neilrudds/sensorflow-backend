using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() { 
            RuleFor(x => x.userName).NotEmpty();
            RuleFor(x => x.firstName).NotEmpty();
            RuleFor(x => x.lastName).NotEmpty();
            RuleFor(x => x.email).NotEmpty();
            RuleFor(x => x.tenantId).NotEmpty();
        }
    }
}