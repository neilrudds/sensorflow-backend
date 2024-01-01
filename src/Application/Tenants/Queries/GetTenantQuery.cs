using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Tenants.Models;

namespace SensorFlow.Application.Tenants.Queries
{
    // Query
    public record GetTenantQuery(string tenantId) : IRequest<TenantDTO>;

    // Query Handler
    public class GetTenantQueryHandler : IRequestHandler<GetTenantQuery, TenantDTO>
    {

        private readonly ITenantRepository _tenantRepository;
        protected readonly IMapper _mapper;

        public GetTenantQueryHandler(IMapper mapper, ITenantRepository repository)
        {
            _tenantRepository = repository;
            _mapper = mapper;
        }

        public async Task<TenantDTO> Handle(GetTenantQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(cancellationToken, request.tenantId);

            // to-do Guard against not found

            return _mapper.Map<TenantDTO>(tenant);
        }
    }
}