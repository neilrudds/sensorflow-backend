using ErrorOr;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IDeviceRepository
    {
        Task<ErrorOr<Device>> GetDeviceByIdAsync(CancellationToken cancellationToken, string deviceId);

        Task<ErrorOr<List<Device>>> GetDevicesByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId);

        Task<ErrorOr<Device>> AddDeviceAsync(CancellationToken cancellationToken, Device toCreate);

        Task<ErrorOr<Device>> UpdateDeviceAsync(CancellationToken cancellationToken, Device toUpdate);

        Task<ErrorOr<Device>> DeleteDeviceAsync(CancellationToken cancellationToken, Device toDelete);

    }
}
