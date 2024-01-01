using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Infrastructure.DbContexts;
using SensorFlow.Application.Common.Models;

/* Concrete implementation of the ITenantRepository */

namespace SensorFlow.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly SensorFlowDbContext _context;

        public TenantRepository(SensorFlowDbContext context)
        {
            _context = context;
        }

        public async Task<(Result result, Tenant tenant)> AddTenantAsync(CancellationToken cancellationToken, Tenant toCreate)
        {
            _context.Tenants.Add(toCreate);

            await _context.SaveChangesAsync(cancellationToken);

            return (Result.Success(), toCreate);
        }

        public async Task DeleteTenantAsync(CancellationToken cancellationToken, Tenant toDelete)
        {
            _context.Tenants.Remove(toDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<Tenant>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Tenants
                .Include(p => p.Workspaces)
                .Include(p => p.Users)
                .Select(s => new Tenant
                {
                    Id = s.Id,
                    Name = s.Name,
                    WorkspaceCount = s.Workspaces.Count,
                    UserCount = s.Users.Count,
                    Workspaces = s.Workspaces,
                    Users = s.Users,
                    OwnerId = s.OwnerId,
                    CreatedTimestamp = s.CreatedTimestamp,
                    ModifiedById = s.ModifiedById,
                    LastModifiedTimestamp = s.LastModifiedTimestamp
                })
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Tenant> GetTenantByIdAsync(CancellationToken cancellationToken, string tenantId)
        {
            var tenant = await _context.Tenants
                .Include(p => p.Workspaces)
                .Include(p => p.Users)
                .Select(s => new Tenant
                {
                    Id = s.Id,
                    Name = s.Name,
                    WorkspaceCount = s.Workspaces.Count,
                    UserCount = s.Users.Count,
                    Workspaces = s.Workspaces,
                    Users = s.Users,
                    OwnerId = s.OwnerId,
                    CreatedTimestamp = s.CreatedTimestamp,
                    ModifiedById = s.ModifiedById,
                    LastModifiedTimestamp = s.LastModifiedTimestamp
                })
                .FirstOrDefaultAsync(p => p.Id == tenantId, cancellationToken);

            if (tenant is null) throw new NotFoundException();

            return tenant;
        }

        public async Task<Tenant> UpdateTenantAsync(CancellationToken cancellationToken, string tenantId, string name)
        {
            var tenant = await _context.Tenants.FirstOrDefaultAsync(p => p.Id == tenantId);
            tenant.Name = name;

            await _context.SaveChangesAsync(cancellationToken);

            return tenant;
        }
    }
}