using FluentValidation;

namespace SensorFlow.Application.Devices.Commands
{
    public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
    {
        public CreateDeviceCommandValidator() {
            RuleFor(x => x.id).MinimumLength(6).MaximumLength(50);
            RuleFor(x => x.name).MinimumLength(6).MaximumLength(50);
            RuleFor(x => x.fields).NotEmpty();
            RuleFor(x => x.workspaceId).NotEmpty();
            RuleFor(x => x.gatewayId).NotEmpty();
        }
    }
}