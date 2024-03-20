using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Dashboards;
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
        public async Task<ErrorOr<Dashboard>> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate)
        {
            _context.Dashboards.Add(toCreate);

            if (await _context.SaveChangesAsync(cancellationToken) < 0)
                return Error.Failure(description: "Unable to save record to Db");

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

        public async Task<ErrorOr<Dashboard>> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId)
        {
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == dashboardId, cancellationToken);

            if (dashboard is null)
                return Error.NotFound(description: "Dashboard not found!");

            return dashboard;
        }

        public async Task<ErrorOr<List<Dashboard>>> GetDashboardsByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            var dashboards = new List<Dashboard>();
            var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null)
                return Error.NotFound(description: "Workspace not found");

            return await _context.Workspaces.Where(w => w.Id == workspaceId).SelectMany(w => w.Dashboards)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, Dashboard toUpdate)
        {
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            dashboard.Name = toUpdate.Name;
            dashboard.WorkspaceId = toUpdate.WorkspaceId;
            dashboard.GridWidgets = toUpdate.GridWidgets;
            dashboard.GridLayout = toUpdate.GridLayout;

            await _context.SaveChangesAsync(cancellationToken);

            return dashboard;
        }
    }
}