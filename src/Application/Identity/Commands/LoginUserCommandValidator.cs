using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator() { 
            RuleFor(x => x.loginRequestDTO.userName).NotEmpty();
            RuleFor(x => x.loginRequestDTO.password).NotEmpty();
        }
    }
}
