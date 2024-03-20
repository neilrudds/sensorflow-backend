using AutoMapper;
using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Gateways.Models;

namespace SensorFlow.Application.Gateways.Queries
{
    // Query
    public record GetWorkspaceGatewaysQuery(string workspaceId) : IRequest<ErrorOr<List<GatewayDTO>>>;

    // Query Handler
    public class GetWorkspaceGatewaysQueryHandler : IRequestHandler<GetWorkspaceGatewaysQuery, ErrorOr<List<GatewayDTO>>>
    {
        private readonly IGatewayRepository _gatewayRepository;
        protected readonly IMapper _mapper;

        public GetWorkspaceGatewaysQueryHandler(IMapper mapper, IGatewayRepository repository)
        {
            _gatewayRepository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<GatewayDTO>>> Handle(GetWorkspaceGatewaysQuery request, CancellationToken cancellationToken)
        {
            var result = await _gatewayRepository.GetGatewaysByWorkspaceIdAsync(cancellationToken, request.workspaceId);

            if (result.IsError)
                return result.Errors;

            return _mapper.Map<List<GatewayDTO>>(result.Value);
        }
    }
}
