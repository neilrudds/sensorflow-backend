using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
    {
        public UpdateUserRolesCommandValidator() { 
            RuleFor(x => x.userName).NotEmpty();
            RuleFor(x => x.roles).NotEmpty();
        }
    }
}
