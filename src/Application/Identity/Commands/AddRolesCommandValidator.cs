using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class AddRolesCommandValidator : AbstractValidator<AddRolesCommand>
    {
        public AddRolesCommandValidator() {
            RuleFor(x => x.userName).NotEmpty();
            RuleFor(x => x.roles).NotEmpty();
        }
    }
}
