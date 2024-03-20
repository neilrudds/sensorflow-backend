using ErrorOr;
using SensorFlow.Domain.Entities.Tenants;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface ITenantRepository
    {
        Task<ICollection<Tenant>> GetAllAsync(CancellationToken cancellationToken);

        Task<Tenant> GetTenantByIdAsync(CancellationToken cancellationToken, string tenantId);

        Task<ErrorOr<Tenant>> AddTenantAsync(CancellationToken cancellationToken, Tenant toCreate);

        Task<Tenant> UpdateTenantAsync(CancellationToken cancellationToken, string tenantId, string name);

        Task DeleteTenantAsync(CancellationToken cancellationToken, Tenant toDelete);
    }
}