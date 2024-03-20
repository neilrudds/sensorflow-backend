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

        public GetDashboardQueryHandler(IMapper mapper, IDashboardRepository repository)
        {
            _dashboardRepository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DashboardDTO>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var result = await _dashboardRepository.GetDashboardByIdAsync(cancellationToken, request.dashboardId);

            if (result.IsError)
                return result.Errors;

            return _mapper.Map<DashboardDTO>(result.Value);
        }
    }
}