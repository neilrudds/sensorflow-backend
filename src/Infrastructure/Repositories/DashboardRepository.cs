using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Infrastructure.DbContexts;

namespace SensorFlow.Infrastructure.Repositories
{
    // Concrete implemetation of the IDashboardRepository
    internal class DashboardRepository : IDashboardRepository
    {
        private readonly SensorFlowDbContext _context;

        public DashboardRepository(SensorFlowDbContext context)
        {
            _context = context;
        }
        // Method to add a Dashboard to the Database
        public async Task<ErrorOr<Dashboard>> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate)
        {
            // Using the database context, add the dashboard object
            _context.Dashboards.Add(toCreate);

            // Then save to DB, return error if they occur
            if (await _context.SaveChangesAsync(cancellationToken) < 0)
                return Error.Failure(description: "Unable to save record to Db");

            return toCreate;
        }

        // Method to delete Dashboard from the database by object
        public async Task DeleteDashboardAsync(CancellationToken cancellationToken, Dashboard toDelete)
        {
            //_context.Workspaces.FirstOrDefault(p => p.Id == toDelete.WorkspaceId).Dashboards.Remove(toDelete);
            _context.Dashboards.Remove(toDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }

        // Method to retrieve all dashboards
        public async Task<ICollection<Dashboard>> GetAllAsync(CancellationToken cancellationToken)
        {
            // Order results by Dashboard Name
            return await _context.Dashboards.OrderBy(p => p.Name).ToListAsync(cancellationToken);
        }

        // Retrieve dashboard by dashboard Id, only one dashboard object should exist
        public async Task<ErrorOr<Dashboard>> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId)
        {
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == dashboardId, cancellationToken);

            // If the dashboard does not exists, return a not found error result
            if (dashboard is null)
                return Error.NotFound(description: "Dashboard not found!");

            return dashboard;
        }

        // Retrieve dashboard by workspace Id
        public async Task<ErrorOr<List<Dashboard>>> GetDashboardsByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            var dashboards = new List<Dashboard>();

            // Check that the provided workspaceId exists
            var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            // If not, return a workspace not found error
            if (workspace is null)
                return Error.NotFound(description: "Workspace not found");

            // Otherwise, retrieve associated dashboards and return as a list of Dashboard objects
            return await _context.Workspaces.Where(w => w.Id == workspaceId).SelectMany(w => w.Dashboards)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);
        }

        // Update dashboard by Id
        public async Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, Dashboard toUpdate)
        {
            // First retrieve the dashboard object from the DB
            var dashboard = await _context.Dashboards
                .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            // Update the provided properties
            dashboard.Name = toUpdate.Name;
            dashboard.WorkspaceId = toUpdate.WorkspaceId;
            dashboard.GridWidgets = toUpdate.GridWidgets;
            dashboard.GridLayout = toUpdate.GridLayout;

            // Save updated Dashboard object to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Return the updated dashboard object
            return dashboard;
        }
    }
}