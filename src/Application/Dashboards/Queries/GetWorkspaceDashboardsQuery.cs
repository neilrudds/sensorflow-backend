using AutoMapper;
using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Dashboards.Models;

namespace SensorFlow.Application.Dashboards.Queries
{
    // Query
    public record GetWorkspaceDashboardsQuery(string workspaceId) : IRequest<ErrorOr<List<DashboardDTO>>>;

    // Query Handler
    public class GetWorkspaceDashboardsQueryHandler : IRequestHandler<GetWorkspaceDashboardsQuery, ErrorOr<List<DashboardDTO>>>
    {
        private readonly IDashboardRepository _dashboardRepository;
        protected readonly IMapper _mapper;

        public GetWorkspaceDashboardsQueryHandler(IMapper mapper, IDashboardRepository repository)
        {
            _dashboardRepository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<DashboardDTO>>> Handle(GetWorkspaceDashboardsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dashboardRepository.GetDashboardsByWorkspaceIdAsync(cancellationToken, request.workspaceId);

            if (result.IsError)
                return result.Errors;

            return _mapper.Map<List<DashboardDTO>>(result.Value);
        }
    }
}