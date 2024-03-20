using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Gateways.Commands
{
    public record UpdateGatewayCommand(string id, string name, string host, int? portNumber, string? username, string? password, string clinetId, bool? sSLEnabled) : IRequest<ErrorOr<Gateway>>;

    public class UpdateGatewayCommandHandler : IRequestHandler<UpdateGatewayCommand, ErrorOr<Gateway>>   
    {
        private readonly IGatewayRepository _gatewayRepository;

        public UpdateGatewayCommandHandler(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        public async Task<ErrorOr<Gateway>> Handle(UpdateGatewayCommand request, CancellationToken cancellationToken)
        {
            var gateway = await _gatewayRepository.GetGatewayByIdAsync(cancellationToken, request.id);

            if (gateway.IsError)
                return gateway.Errors;

            if (!String.IsNullOrEmpty(request.name))
                gateway.Value.UpdateGatewayName(request.name);

            return await _gatewayRepository.UpdateGatewayAsync(cancellationToken, gateway.Value);
        }
    }
}
