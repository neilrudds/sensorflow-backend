using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Devices.Commands
{
    // Command
    public record DeleteDeviceCommand(string id) : IRequest<ErrorOr<Device>>;

    // Command Handler
    public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, ErrorOr<Device>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeleteDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<ErrorOr<Device>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            var toDelete = await _deviceRepository.GetDeviceByIdAsync(cancellationToken, request.id);

            if (toDelete.IsError)
                return toDelete.Errors;

            return await _deviceRepository.DeleteDeviceAsync(cancellationToken, toDelete.Value);
        }
    }
}