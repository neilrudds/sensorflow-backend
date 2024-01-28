using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
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
        public async Task<(Result result, Dashboard dashboard)> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate)
        {
            _context.Dashboards.Add(toCreate);

            if (await _context.SaveChangesAsync(cancellationToken) < 0)
                return (Result.Failure("Unable to save record to Db"), toCreate);

            return (Result.Success(), toCreate);
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

        public async Task<(Result result, Dashboard dashboard)> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId)
        {
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == dashboardId, cancellationToken);

            if (dashboard is null)
                return (Result.Failure("Dashboard not found!"), new Dashboard { });

            return (Result.Success(), dashboard);
        }

        public async Task<(Result result, List<Dashboard> dashboards)> GetDashboardsByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {

            var dashboards = new List<Dashboard>();
            var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null)
                return (Result.Failure("Workspace not found!"), dashboards);

            dashboards = await _context.Workspaces.Where(w => w.Id == workspaceId).SelectMany(w => w.Dashboards)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);

            return (Result.Success(), dashboards);
        }

        public async Task<(Result result, Dashboard dashboard)> UpdateDashboardAsync(CancellationToken cancellationToken, Dashboard toUpdate)
        {
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            if (dashboard is null)
                return (Result.Failure("Dashboard not found!"), new Dashboard { });

            dashboard.Name = toUpdate.Name;
            dashboard.WorkspaceId = toUpdate.WorkspaceId;
            dashboard.GridWidgets = toUpdate.GridWidgets;
            dashboard.GridLayout = toUpdate.GridLayout;

            await _context.SaveChangesAsync(cancellationToken);

            return (Result.Success(), dashboard);
        }
    }
}