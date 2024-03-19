using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Devices.Commands
{

    // Command
    public record CreateDeviceCommand(string id, string name, string fields, string workspaceId, string gatewayId) : IRequest<(Result result, Device device)>;

    // Command Handler
    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, (Result result, Device device)>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IWorkspaceRepository _workspaceRepository;


        public CreateDeviceCommandHandler(IDeviceRepository deviceRepository, IWorkspaceRepository workspaceRepository)
        {
            _deviceRepository = deviceRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<(Result result, Device? device)> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            //var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            //if (workspace is null)
            //    return (Result.Failure("Unable to locate workspace"),  null);

            var device = Device.CreateDevice(
                request.id,
                request.name,
                request.fields,
                request.workspaceId,
                request.gatewayId
            );

            return await _deviceRepository.AddDeviceAsync(cancellationToken, device);
        }
    }

}
