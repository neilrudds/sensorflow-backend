using FluentValidation;

namespace SensorFlow.Application.Devices.Commands
{
    public class DeleteDeviceCommandValidator : AbstractValidator<DeleteDeviceCommand>
    {
        public DeleteDeviceCommandValidator()
        {
            RuleFor(x => x.id).MinimumLength(6).MinimumLength(50);
        }
    }
}