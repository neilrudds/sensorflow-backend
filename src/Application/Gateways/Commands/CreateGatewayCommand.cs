using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Gateways.Commands
{

    // Command
    public record CreateGatewayCommand(string name, string workspaceId, string host) : IRequest<ErrorOr<Gateway>>;

    // Command Handler
    public class CreateGatewayCommandHandler : IRequestHandler<CreateGatewayCommand, ErrorOr<Gateway>>
    {
        private readonly IGatewayRepository _gatewayRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateGatewayCommandHandler(IGatewayRepository gatewayRepository, IWorkspaceRepository workspaceRepository)
        {
            _gatewayRepository = gatewayRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<ErrorOr<Gateway>> Handle(CreateGatewayCommand request, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            if (workspace.IsError)
                return workspace.Errors;

            var gateway = Gateway.CreateGateway(
                request.name,
                request.workspaceId,
                request.host
            );

            return await _gatewayRepository.AddGatewayAsync(cancellationToken, gateway);
        }
    }

}
