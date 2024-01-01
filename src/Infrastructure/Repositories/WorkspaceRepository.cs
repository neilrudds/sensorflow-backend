using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Infrastructure.DbContexts;
using SensorFlow.Application.Common.Models;

/* Concrete implementation of the IWorkspaceRepository */

namespace SensorFlow.Infrastructure.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly SensorFlowDbContext _context;

        public WorkspaceRepository(SensorFlowDbContext context)
        {
            _context = context;
        }

        public async Task<(Result result, Workspace workspace)> AddWorkspaceAsync(CancellationToken cancellationToken, Workspace toCreate)
        {
            _context.Workspaces.Add(toCreate);

            await _context.SaveChangesAsync(cancellationToken);

            return (Result.Success(), toCreate);
        }

        public async Task DeleteWorkspaceAsync(CancellationToken cancellationToken, Workspace toDelete)
        {
            _context.Workspaces.Remove(toDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<Workspace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Workspaces
                .Include(p => p.Dashboards)
                .Include(p => p.Devices)
                .Select(s => new Workspace
                {
                    Id = s.Id,
                    Name = s.Name,
                    TenantId = s.TenantId,
                    DeviceCount = s.Devices.Count,
                    UserCount = s.Devices.Count,
                    Dashboards = s.Dashboards,
                    Devices = s.Devices,
                    OwnerId = s.OwnerId,
                    CreatedTimestamp = s.CreatedTimestamp,
                    ModifiedById = s.ModifiedById,
                    LastModifiedTimestamp = s.LastModifiedTimestamp
                })
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Workspace> GetWorkspaceByIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            var workspace = await _context.Workspaces
                .Include(p => p.Dashboards)
                .Include(p => p.Devices)
                .Select(s => new Workspace
                {
                    Id = s.Id,
                    Name = s.Name,
                    TenantId = s.TenantId,
                    DeviceCount = s.Devices.Count,
                    UserCount = s.Devices.Count,
                    Dashboards = s.Dashboards,
                    Devices = s.Devices,
                    OwnerId = s.OwnerId,
                    CreatedTimestamp = s.CreatedTimestamp,
                    ModifiedById = s.ModifiedById,
                    LastModifiedTimestamp = s.LastModifiedTimestamp
                })
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null) throw new NotFoundException();

            return workspace;
        }

        public async Task<Workspace> UpdateWorkspaceAsync(CancellationToken cancellationToken, string workspaceId, string name)
        {
            var workspace = await _context.Workspaces.FirstOrDefaultAsync(p => p.Id == workspaceId);
            workspace.Name = name;

            await _context.SaveChangesAsync(cancellationToken);

            return workspace;
        }
    }
}