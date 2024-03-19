using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Gateways.Commands
{

    // Command
    public record CreateGatewayCommand(string name, string workspaceId, string host) : IRequest<(Result result, Gateway gateway)>;

    // Command Handler
    public class CreateGatewayCommandHandler : IRequestHandler<CreateGatewayCommand, (Result result, Gateway gateway)>
    {
        private readonly IGatewayRepository _gatewayRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateGatewayCommandHandler(IGatewayRepository gatewayRepository, IWorkspaceRepository workspaceRepository)
        {
            _gatewayRepository = gatewayRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<(Result result, Gateway? gateway)> Handle(CreateGatewayCommand request, CancellationToken cancellationToken)
        {
            //var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            //if (workspace is null)
            //    return (Result.Failure("Unable to locate workspace"),  null);

            var gateway = Gateway.CreateGateway(
                request.name,
                request.workspaceId,
                request.host
            );

            return await _gatewayRepository.AddGatewayAsync(cancellationToken, gateway);
        }
    }

}
