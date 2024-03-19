using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Gateways.Models;

namespace SensorFlow.Application.Gateways.Queries
{
    // Query
    public record GetWorkspaceGatewaysQuery(string workspaceId) : IRequest<(Result result, List<GatewayDTO> gateways)>;

    // Query Handler
    public class GetWorkspaceGatewaysQueryHandler : IRequestHandler<GetWorkspaceGatewaysQuery, (Result result, List<GatewayDTO> gateways)>
    {
        private readonly IGatewayRepository _gatewayRepository;
        protected readonly IMapper _mapper;

        public GetWorkspaceGatewaysQueryHandler(IMapper mapper, IGatewayRepository repository)
        {
            _gatewayRepository = repository;
            _mapper = mapper;
        }

        public async Task<(Result result, List<GatewayDTO> gateways)> Handle(GetWorkspaceGatewaysQuery request, CancellationToken cancellationToken)
        {
            var result = await _gatewayRepository.GetGatewaysByWorkspaceIdAsync(cancellationToken, request.workspaceId);

            // to-do Guard against not found

            return (result.result, _mapper.Map<List<GatewayDTO>>(result.gateways));
        }
    }
}
