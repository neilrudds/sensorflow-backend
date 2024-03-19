using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Devices;
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

        public async Task<(Result result, Gateway device)> AddGatewayAsync(CancellationToken cancellationToken, Gateway toCreate)
        {
            _context.Gateways.Add(toCreate);

            if (await _context.SaveChangesAsync(cancellationToken) < 0)
                return (Result.Failure("Unable to save record to Db"), toCreate);

            return (Result.Success(), toCreate);
        }

        public async Task<int> DeleteGatewayAsync(CancellationToken cancellationToken, Gateway toDelete)
        {
            _context.Gateways.Remove(toDelete);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(Result result, Gateway gateway)> GetGatewayByIdAsync(CancellationToken cancellationToken, string gatewayId)
        {
            var gateway = await _context.Gateways
                .FirstOrDefaultAsync(p => p.Id == gatewayId, cancellationToken);

            if (gateway is null)
                return (Result.Failure("Gateway not found!"), new Gateway { });

            return (Result.Success(), gateway);
        }

        public async Task<(Result result, List<Gateway> gateways)> GetGatewaysByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            var gateways = new List<Gateway>();
            var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null)
                return (Result.Failure("Workspace not found!"), gateways);

            gateways = await _context.Workspaces.Where(w => w.Id == workspaceId).SelectMany(w => w.Gateways)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);

            return (Result.Success(), gateways);
        }

        public async Task<(Result result, Gateway device)> UpdateGatewayAsync(CancellationToken cancellationToken, Gateway toUpdate)
        {
            var gateway = await _context.Gateways
                .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            if (gateway is null)
                return (Result.Failure("Gateway not found!"), new Gateway { });

            gateway.Name = toUpdate.Name;
            gateway.Host = toUpdate.Host;
            gateway.PortNumber = toUpdate.PortNumber;
            gateway.Username = toUpdate.Username;
            gateway.Password = toUpdate.Password;
            gateway.ClientId = toUpdate.ClientId;
            gateway.SSLEnabled = toUpdate.SSLEnabled;

            await _context.SaveChangesAsync(cancellationToken);

            return (Result.Success(), gateway);
        }
    }
}
