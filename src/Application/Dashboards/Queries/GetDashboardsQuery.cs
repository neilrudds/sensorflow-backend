using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Dashboards.Models;

namespace SensorFlow.Application.Dashboards.Queries
{
    // Query
    public record GetDashboardsQuery() : IRequest<List<DashboardDTO>>;

    // Query Handler
    public class GetDashboardsQueryHandler : IRequestHandler<GetDashboardsQuery, List<DashboardDTO>>
    {

        private readonly IDashboardRepository _dashboardRepository;
        protected readonly IMapper _mapper;

        public GetDashboardsQueryHandler(IMapper mapper, IDashboardRepository repository)
        {
            _dashboardRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<DashboardDTO>> Handle(GetDashboardsQuery request, CancellationToken cancellationToken)
        {
            var dashboards = await _dashboardRepository.GetAllAsync(cancellationToken);

            // to-do Guard against not found

            return _mapper.Map<List<DashboardDTO>>(dashboards);
        }
    }
}