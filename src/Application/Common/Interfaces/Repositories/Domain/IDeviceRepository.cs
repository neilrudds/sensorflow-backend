using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IDeviceRepository
    {
        Task<(Result result, Device device)> GetDeviceByIdAsync(CancellationToken cancellationToken, string deviceId);

        Task<(Result result, List<Device> devices)> GetDevicesByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId);

        Task<(Result result, Device device)> AddDeviceAsync(CancellationToken cancellationToken, Device toCreate);

        Task<(Result result, Device device)> UpdateDeviceAsync(CancellationToken cancellationToken, Device toUpdate);

        Task<int> DeleteDeviceAsync(CancellationToken cancellationToken, Device toDelete);

    }
}
