using AutoMapper;
using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Dashboards.Models;

namespace SensorFlow.Application.Dashboards.Queries
{
    // Query
    public record GetDashboardQuery(string dashboardId) : IRequest<ErrorOr<DashboardDTO>>;

    // Query Handler
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, ErrorOr<DashboardDTO>>
    {

        private readonly IDashboardRepository _dashboardRepository;
        protected readonly IMapper _mapper;

        // Repositories will be injected via dependancy injection
        public GetDashboardQueryHandler(IMapper mapper, IDashboardRepository repository)
        {
            _dashboardRepository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DashboardDTO>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            // Request the Dashboard object from the database via infrastructure layer repository by database Id
            var result = await _dashboardRepository.GetDashboardByIdAsync(cancellationToken, request.dashboardId);

            // Where errors exists, return these via the ErrorOr object
            if (result.IsError)
                return result.Errors;

            // Otherwise, map the Dashboard to a DashboardDTO object using mapper and return via the ErrorOr object.
            return _mapper.Map<DashboardDTO>(result.Value);
        }
    }
}