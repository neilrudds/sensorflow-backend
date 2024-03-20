using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Infrastructure.DbContexts;
using SensorFlow.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using SensorFlow.Domain.Entities.Users;
using ErrorOr;

/* Concrete implementation of the IWorkspaceRepository */

namespace SensorFlow.Infrastructure.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly SensorFlowDbContext _context;
        private readonly UserManager<User> _userManager;

        public WorkspaceRepository(SensorFlowDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
    }

        public async Task<ErrorOr<Workspace>> AddWorkspaceAsync(CancellationToken cancellationToken, Workspace toCreate)
        {
            _context.Workspaces.Add(toCreate);

            await _context.SaveChangesAsync(cancellationToken);

            return (toCreate);
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
                    UserCount = s.Users.Count,
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
                    UserCount = s.Users.Count,
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

        public async Task<ErrorOr<List<Workspace>>> GetWorkspacesByUsernameAsync(CancellationToken cancellationToken, string username)
        {

            var workspaces = new List<Workspace>();
            var applicationUser = await _userManager.FindByNameAsync(username);

            if (applicationUser == null)
                return Error.NotFound(description: "User not found");

            return await _context.Users.Where(u => u.UserName == username).SelectMany(w => w.Workspaces)
                .Include(p => p.Dashboards)
                .Include(p => p.Devices)
                .Select(s => new Workspace
                {
                    Id = s.Id,
                    Name = s.Name,
                    TenantId = s.TenantId,
                    DeviceCount = s.Devices.Count,
                    UserCount = s.Users.Count,
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

        public async Task<Workspace> UpdateWorkspaceAsync(CancellationToken cancellationToken, string workspaceId, string name)
        {
            var workspace = await _context.Workspaces.FirstOrDefaultAsync(p => p.Id == workspaceId);
            workspace.Name = name;

            await _context.SaveChangesAsync(cancellationToken);

            return workspace;
        }
    }
}