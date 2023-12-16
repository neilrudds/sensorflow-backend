using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Abstractions.Exceptions;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Infrastructure.DbContexts;

namespace SensorFlow.Infrastructure.Repositories
{
    internal class DashboardRepository : IDashboardRepository
    {
        private readonly SensorFlowDbContext _context;

        public DashboardRepository(SensorFlowDbContext context)
        {
            _context = context;
        }
        public async Task<Dashboard> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate)
        {
            _context.Dashboards.Add(toCreate);
            //_context.Workspaces.FirstOrDefault(p => p.Id == toCreate.WorkspaceId).Dashboards.Add(toCreate);

            await _context.SaveChangesAsync(cancellationToken);

            return toCreate;
        }

        public async Task DeleteDashboardAsync(CancellationToken cancellationToken, Dashboard toDelete)
        {
            //_context.Workspaces.FirstOrDefault(p => p.Id == toDelete.WorkspaceId).Dashboards.Remove(toDelete);
            _context.Dashboards.Remove(toDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<Dashboard>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Dashboards.OrderBy(p => p.Name).ToListAsync(cancellationToken);
        }

        public async Task<Dashboard> GetDashboardByIdAsync(CancellationToken cancellationToken, Guid dashboardId)
        {
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == dashboardId, cancellationToken);

            if (dashboard is null) throw new NotFoundException();

            return dashboard;
        }

        public async Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, Guid dashboardId, string name, DateTime lastModified)
        {
            var dashboard = await _context.Dashboards.FirstOrDefaultAsync(p => p.Id == dashboardId);
            dashboard.Name = name;
            dashboard.LastModified = lastModified;

            await _context.SaveChangesAsync(cancellationToken);

            return dashboard;
        }
    }
}