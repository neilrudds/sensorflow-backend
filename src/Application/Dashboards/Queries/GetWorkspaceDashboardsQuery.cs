using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Dashboards.Models;

namespace SensorFlow.Application.Dashboards.Queries
{
    // Query
    public record GetWorkspaceDashboardsQuery(string workspaceId) : IRequest<(Result result, List<DashboardDTO> dashboards)>;

    // Query Handler
    public class GetWorkspaceDashboardsQueryHandler : IRequestHandler<GetWorkspaceDashboardsQuery, (Result result, List<DashboardDTO> dashboards)>
    {
        private readonly IDashboardRepository _dashboardRepository;
        protected readonly IMapper _mapper;

        public GetWorkspaceDashboardsQueryHandler(IMapper mapper, IDashboardRepository repository)
        {
            _dashboardRepository = repository;
            _mapper = mapper;
        }

        public async Task<(Result result, List<DashboardDTO> dashboards)> Handle(GetWorkspaceDashboardsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dashboardRepository.GetDashboardsByWorkspaceIdAsync(cancellationToken, request.workspaceId);

            // to-do Guard against not found

            return (result.result, _mapper.Map<List<DashboardDTO>>(result.dashboards));
        }
    }
}