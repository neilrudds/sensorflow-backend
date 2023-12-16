using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Infrastructure.DbContexts;

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

        public async Task<Workspace> AddWorkspaceAsync(CancellationToken cancellationToken, Workspace toCreate)
        {
            _context.Workspaces.Add(toCreate);

            await _context.SaveChangesAsync(cancellationToken);

            return toCreate;
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
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);
        }

        public async Task<Workspace> GetWorkspaceByIdAsync(CancellationToken cancellationToken, Guid workspaceId)
        {
            var workspace = await _context.Workspaces
                .Include(p => p.Dashboards)
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null) throw new NotFoundException();

            return workspace;
        }

        public async Task<Workspace> UpdateWorkspaceAsync(CancellationToken cancellationToken, Guid workspaceId, string name, DateTime lastModified)
        {
            var workspace = await _context.Workspaces.FirstOrDefaultAsync(p => p.Id == workspaceId);
            workspace.Name = name;
            workspace.LastModified = lastModified;

            await _context.SaveChangesAsync(cancellationToken);

            return workspace;
        }
    }
}