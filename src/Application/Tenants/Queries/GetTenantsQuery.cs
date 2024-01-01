using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Tenants.Models;

namespace SensorFlow.Application.Tenants.Queries
{
    // Query
    public record GetTenantsQuery() : IRequest<List<TenantDTO>>;

    // Query Handler
    public class GetTenantsQueryHandler : IRequestHandler<GetTenantsQuery, List<TenantDTO>>
    {

        private readonly ITenantRepository _tenantRepository;
        protected readonly IMapper _mapper;

        public GetTenantsQueryHandler(IMapper mapper, ITenantRepository repository)
        {
            _tenantRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<TenantDTO>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantRepository.GetAllAsync(cancellationToken);

            // to-do Guard against not found

            return _mapper.Map<List<TenantDTO>>(tenants);
        }
    }
}