using AutoMapper;
using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Gateways.Models;

namespace SensorFlow.Application.Gateways.Queries
{
    // Query
    public record GetGatewayQuery(string gatewayId) : IRequest<ErrorOr<GatewayDTO>>;

    // Query Handler
    public class GetGatewayQueryHandler : IRequestHandler<GetGatewayQuery, ErrorOr<GatewayDTO>>
    {
        private readonly IGatewayRepository _gatewayRepository;
        protected readonly IMapper _mapper;

        public GetGatewayQueryHandler(IMapper mapper, IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GatewayDTO>> Handle(GetGatewayQuery request, CancellationToken cancellationToken)
        {
            var result = await _gatewayRepository.GetGatewayByIdAsync(cancellationToken, request.gatewayId);

            if (result.IsError)
                return result.Errors;

            return _mapper.Map<GatewayDTO>(result.Value);
        }
    }
}