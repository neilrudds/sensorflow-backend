using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
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

        public async Task<ErrorOr<Device>> GetDeviceByIdAsync(CancellationToken cancellationToken, string deviceId)
        {
            var device = await _context.Devices
                .FirstOrDefaultAsync(p => p.Id == deviceId, cancellationToken);

            if (device is null)
                return Error.NotFound(description: "Device not found!");

            return device;
        }

        public async Task<ErrorOr<Device>> AddDeviceAsync(CancellationToken cancellationToken, Device toCreate)
        {
            _context.Devices.Add(toCreate);

            if (await _context.SaveChangesAsync(cancellationToken) < 0)
                return Error.Failure(description: "Unable to save record to Db");

            return toCreate;
        }

        public async Task<ErrorOr<Device>> DeleteDeviceAsync(CancellationToken cancellationToken, Device toDelete)
        {
            _context.Devices.Remove(toDelete);

            if (await _context.SaveChangesAsync(cancellationToken) < 1)
                return Error.Failure(description: "Unable to delete device");

            return toDelete;
        }

        public async Task<ErrorOr<List<Device>>> GetDevicesByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            var devices = new List<Device>();
            var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(p => p.Id == workspaceId, cancellationToken);

            if (workspace is null)
                return Error.NotFound(description: "Workspace not found!");

           return await _context.Workspaces.Where(w => w.Id == workspaceId).SelectMany(w => w.Devices)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<ErrorOr<Device>> UpdateDeviceAsync(CancellationToken cancellationToken, Device toUpdate)
        {
            var device = await _context.Devices
                .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            if (device is null)
                return Error.NotFound(description: "Device not found!");

            device.Name = toUpdate.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return device;
        }
    }
}
