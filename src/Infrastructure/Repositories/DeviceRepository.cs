using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Devices;
using SensorFlow.Infrastructure.DbContexts;

namespace SensorFlow.Infrastructure.Repositories
{
    internal class DeviceRepository : IDeviceRepository
    {
        private readonly SensorFlowDbContext _context;

        public DeviceRepository(SensorFlowDbContext context)
        {
            _context = context;
        }

        public async Task<(Result result, Device device)> GetDeviceByIdAsync(CancellationToken cancellationToken, string deviceId)
        {
            var device = await _context.Devices
                .FirstOrDefaultAsync(p => p.Id == deviceId, cancellationToken);

            if (device is null)
                return (Result.Failure("Device not found!"), new Device { });

            return (Result.Success(), device);
        }

        public async Task<(Result result, Device device)> AddDeviceAsync(CancellationToken cancellationToken, Device toCreate)
        {
            _context.Devices.Add(toCreate);

            if (await _context.SaveChangesAsync(cancellationToken) < 0)
                return (Result.Failure("Unable to save record to Db"), toCreate);

            return (Result.Success(), toCreate);
        }

        public async Task<int> DeleteDeviceAsync(CancellationToken cancellationToken, Device toDelete)
        {
            _context.Devices.Remove(toDelete);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(Result result, List<Device> devices)> GetDevicesByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            var devices = new List<Device>();
            var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null)
                return (Result.Failure("Workspace not found!"), devices);

            devices = await _context.Workspaces.Where(w => w.Id == workspaceId).SelectMany(w => w.Devices)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);

            return (Result.Success(), devices);
        }

        public async Task<(Result result, Device device)> UpdateDeviceAsync(CancellationToken cancellationToken, Device toUpdate)
        {
            var device = await _context.Devices
                .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            if (device is null)
                return (Result.Failure("Device not found!"), new Device { });

            device.Name = toUpdate.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return (Result.Success(), device);
        }
    }
}
