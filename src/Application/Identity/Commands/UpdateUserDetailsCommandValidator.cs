using FluentValidation;

namespace SensorFlow.Application.Identity.Commands
{
    public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        public UpdateUserDetailsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.email).MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.firstName).MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.lastName).MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.phoneNumber).MinimumLength(2).MaximumLength(20);
        }
    }
}