using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Devices.Commands
{
    public record UpdateDeviceCommand(string deviceId, string? name, string? fields, string gatewayId) : IRequest<(Result result, Device device)>;
    
    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, (Result result, Device device)>
    {
        private readonly IDeviceRepository _deviceRepository;

        public UpdateDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<(Result result, Device device)> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = _deviceRepository.GetDeviceByIdAsync(cancellationToken, request.deviceId);

            if (device == null)
                return (Result.Failure("Device not found."), new Device { });

            if (!String.IsNullOrEmpty(request.name))
                device.Result.device.UpdateDeviceName(request.name);

            if (!String.IsNullOrEmpty(request.fields))
                device.Result.device.UpdateFields(request.fields);

            if (!String.IsNullOrEmpty(request.gatewayId))
                device.Result.device.UpdateDeviceGatewayId(request.gatewayId);

            return await _deviceRepository.UpdateDeviceAsync(cancellationToken, device.Result.device);
        }
    }
}
