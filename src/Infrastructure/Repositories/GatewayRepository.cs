using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Gateways;
using SensorFlow.Infrastructure.DbContexts;

namespace SensorFlow.Infrastructure.Repositories
{
    internal class GatewayRepository : IGatewayRepository
    {
        private readonly SensorFlowDbContext _context;

        public GatewayRepository(SensorFlowDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Gateway>> AddGatewayAsync(CancellationToken cancellationToken, Gateway toCreate)
        {
            _context.Gateways.Add(toCreate);

            if (await _context.SaveChangesAsync(cancellationToken) < 1)
                return Error.NotFound(description: "Unable to save record to Db");

            return toCreate;
        }

        public async Task<ErrorOr<Gateway>> DeleteGatewayAsync(CancellationToken cancellationToken, Gateway toDelete)
        {
            _context.Gateways.Remove(toDelete);

            if (await _context.SaveChangesAsync(cancellationToken) < 1)
                return Error.Failure(description: "Unable to delete gateway");

            return toDelete;
        }

        public async Task<ErrorOr<Gateway>> GetGatewayByIdAsync(CancellationToken cancellationToken, string gatewayId)
        {
            var gateway = await _context.Gateways
                .FirstOrDefaultAsync(p => p.Id == gatewayId, cancellationToken);

            if (gateway is null)
                return Error.NotFound(description: "Gateway not found!");

            return gateway;
        }

        public async Task<ErrorOr<List<Gateway>>> GetGatewaysByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            var gateways = new List<Gateway>();
            var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null)
                return Error.NotFound(description: "Workspace not found!");

            return await _context.Workspaces.Where(w => w.Id == workspaceId).SelectMany(w => w.Gateways)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<ErrorOr<Gateway>> UpdateGatewayAsync(CancellationToken cancellationToken, Gateway toUpdate)
        {
            var gateway = await _context.Gateways
                .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            if (gateway is null)
                return Error.NotFound(description: "Gateway not found!");

            gateway.Name = toUpdate.Name;
            gateway.Host = toUpdate.Host;
            gateway.PortNumber = toUpdate.PortNumber;
            gateway.Username = toUpdate.Username;
            gateway.Password = toUpdate.Password;
            gateway.ClientId = toUpdate.ClientId;
            gateway.SSLEnabled = toUpdate.SSLEnabled;

            if (await _context.SaveChangesAsync(cancellationToken) < 0)
                Error.Failure(description: "Unable to save gateway");

            return gateway;
        }
    }
}
