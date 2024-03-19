using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Gateways.Commands
{
    public record UpdateGatewayCommand(string id, string name, string host, int? portNumber, string? username, string? password, string clinetId, bool? sSLEnabled) : IRequest<(Result result, Gateway gateway)>;

    public class UpdateGatewayCommandHandler : IRequestHandler<UpdateGatewayCommand, (Result result, Gateway gateway)>
    {
        private readonly IGatewayRepository _gatewayRepository;

        public UpdateGatewayCommandHandler(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        public async Task<(Result result, Gateway gateway)> Handle(UpdateGatewayCommand request, CancellationToken cancellationToken)
        {
            var gateway = _gatewayRepository.GetGatewayByIdAsync(cancellationToken, request.id);

            if (gateway == null)
                return (Result.Failure("Gateway not found."), new Gateway { });

            if (!String.IsNullOrEmpty(request.name))
                gateway.Result.gateway.UpdateGatewayName(request.name);

            return await _gatewayRepository.UpdateGatewayAsync(cancellationToken, gateway.Result.gateway);
        }
    }
}
