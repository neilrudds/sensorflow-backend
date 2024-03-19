using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Gateways.Commands
{
    // Command
    public record DeleteGatewayCommand(string id) : IRequest<Result>;

    // Command Handler
    public class DeleteGatewayCommandHandler : IRequestHandler<DeleteGatewayCommand, Result>
    {
        private readonly IGatewayRepository _gatewayRepository;

        public DeleteGatewayCommandHandler(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        public async Task<Result> Handle(DeleteGatewayCommand request, CancellationToken cancellationToken)
        {
            var toDelete = _gatewayRepository.GetGatewayByIdAsync(cancellationToken, request.id);

            if (toDelete == null)
                return (Result.Success("Gateway not found."));

            await _gatewayRepository.DeleteGatewayAsync(cancellationToken, toDelete.Result.gateway);
            return (Result.Success("Gateway deleted."));
        }
    }
}