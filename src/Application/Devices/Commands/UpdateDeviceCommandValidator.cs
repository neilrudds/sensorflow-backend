using FluentValidation;

namespace SensorFlow.Application.Devices.Commands
{
    public class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
    {
        public UpdateDeviceCommandValidator()
        {
            RuleFor(x => x.id).MinimumLength(6).MinimumLength(50);
            RuleFor(x => x.name).MinimumLength(6).MaximumLength(50);
            RuleFor(x => x.fields).NotEmpty();
            RuleFor(x => x.gatewayId).NotEmpty();
        }
    }
}