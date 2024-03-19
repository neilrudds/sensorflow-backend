using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Devices.Commands
{
    // Command
    public record DeleteDeviceCommand(string id) : IRequest<Result>;

    // Command Handler
    public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, Result>
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeleteDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Result> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            var toDelete = _deviceRepository.GetDeviceByIdAsync(cancellationToken, request.id);

            if (toDelete == null)
                return (Result.Success("Device not found."));

            await _deviceRepository.DeleteDeviceAsync(cancellationToken, toDelete.Result.device);
            return (Result.Success("Device deleted."));
        }
    }
}