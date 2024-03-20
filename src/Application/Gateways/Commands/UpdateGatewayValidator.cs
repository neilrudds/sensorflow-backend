using FluentValidation;

namespace SensorFlow.Application.Gateways.Commands
{
    public class UpdateGatewayValidator : AbstractValidator<UpdateGatewayCommand>
    {
        public UpdateGatewayValidator() {
            RuleFor(x => x.id).NotEmpty();
            RuleFor(x => x.name).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.host).MinimumLength(8).MaximumLength(50);
            RuleFor(x => x.portNumber).Null();
            RuleFor(x => x.username).MaximumLength(50);
            RuleFor(x => x.password).MaximumLength(50);
            RuleFor(x => x.clinetId).MinimumLength(3).MaximumLength(50);
        }
    }
}