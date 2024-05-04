using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Devices.Commands
{
    public record UpdateDeviceCommand(string id, string? name, string? location, string? fields, string gatewayId) : IRequest<ErrorOr<Device>>;
    
    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, ErrorOr<Device>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public UpdateDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<ErrorOr<Device>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(cancellationToken, request.id);

            if (device.IsError)
                return device.Errors;

            if (!String.IsNullOrEmpty(request.name))
                device.Value.UpdateDeviceName(request.name);

            if (!String.IsNullOrEmpty(request.location))
                device.Value.UpdateDeviceLocation(request.location);

            if (!String.IsNullOrEmpty(request.fields))
                device.Value.UpdateFields(request.fields);

            if (!String.IsNullOrEmpty(request.gatewayId))
                device.Value.UpdateDeviceGatewayId(request.gatewayId);

            return await _deviceRepository.UpdateDeviceAsync(cancellationToken, device.Value);
        }
    }
}
