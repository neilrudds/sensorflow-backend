using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Devices.Commands
{

    // Command
    public record CreateDeviceCommand(string id, string name, string location, string fields, string workspaceId, string gatewayId) : IRequest<ErrorOr<Device>>;

    // Command Handler
    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, ErrorOr<Device>>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateDeviceCommandHandler(IDeviceRepository deviceRepository, IWorkspaceRepository workspaceRepository)
        {
            _deviceRepository = deviceRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<ErrorOr<Device>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            if (workspace.IsError)
                return workspace.Errors;

            var deviceExists = await _deviceRepository.GetDeviceByIdAsync(cancellationToken, request.id);

            if (deviceExists.Value is not null)
                return Error.Conflict(description: "Device serial already exists");

            var device = Device.CreateDevice(
                request.id,
                request.name,
                request.location,
                request.fields,
                request.workspaceId,
                request.gatewayId
            );

            if (device.IsError)
                return device.Errors;

            return await _deviceRepository.AddDeviceAsync(cancellationToken, device.Value);
        }
    }

}
