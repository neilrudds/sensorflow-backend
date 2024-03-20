using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Gateways.Commands
{
    // Command
    public record DeleteGatewayCommand(string id) : IRequest<ErrorOr<Gateway>>;

    // Command Handler
    public class DeleteGatewayCommandHandler : IRequestHandler<DeleteGatewayCommand, ErrorOr<Gateway>>
    {
        private readonly IGatewayRepository _gatewayRepository;

        public DeleteGatewayCommandHandler(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        public async Task<ErrorOr<Gateway>> Handle(DeleteGatewayCommand request, CancellationToken cancellationToken)
        {
            var toDelete = await _gatewayRepository.GetGatewayByIdAsync(cancellationToken, request.id);

            if (toDelete.IsError)
                return toDelete.Errors;

            return await _gatewayRepository.DeleteGatewayAsync(cancellationToken, toDelete.Value);
        }
    }
}