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

        public async Task<Dashboard> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId)
        {
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == dashboardId, cancellationToken);

            if (dashboard is null) throw new NotFoundException();

            return dashboard;
        }

        public async Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, string dashboardId, string name)
        {
            var dashboard = await _context.Dashboards.FirstOrDefaultAsync(p => p.Id == dashboardId);
            dashboard.Name = name;

            await _context.SaveChangesAsync(cancellationToken);

            return dashboard;
        }
    }
}