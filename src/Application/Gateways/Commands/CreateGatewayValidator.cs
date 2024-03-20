using FluentValidation;

namespace SensorFlow.Application.Gateways.Commands
{
    public class CreateGatewayValidator : AbstractValidator<CreateGatewayCommand>
    {
        public CreateGatewayValidator() {
            RuleFor(x => x.name).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.workspaceId).NotEmpty();
            RuleFor(x => x.host).MinimumLength(8).MaximumLength(50);
        }
    }
}