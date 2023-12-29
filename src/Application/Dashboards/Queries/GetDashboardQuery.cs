using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Dashboards.Models;

namespace SensorFlow.Application.Dashboards.Queries
{
    // Query
    public record GetDashboardQuery(string dashboardId) : IRequest<DashboardDTO>;

    // Query Handler
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDTO>
    {

        private readonly IDashboardRepository _dashboardRepository;
        protected readonly IMapper _mapper;

        public GetDashboardQueryHandler(IMapper mapper, IDashboardRepository repository)
        {
            _dashboardRepository = repository;
            _mapper = mapper;
        }

        public async Task<DashboardDTO> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboard = await _dashboardRepository.GetDashboardByIdAsync(cancellationToken, request.dashboardId);

            // to-do Guard against not found

            return _mapper.Map<DashboardDTO>(dashboard);
        }
    }
}