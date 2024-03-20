using ErrorOr;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IGatewayRepository
    {
        Task<ErrorOr<Gateway>> GetGatewayByIdAsync(CancellationToken cancellationToken, string gatewayId);

        Task<ErrorOr<List<Gateway>>> GetGatewaysByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId);

        Task<ErrorOr<Gateway>> AddGatewayAsync(CancellationToken cancellationToken, Gateway toCreate);

        Task<ErrorOr<Gateway>> UpdateGatewayAsync(CancellationToken cancellationToken, Gateway toUpdate);

        Task<ErrorOr<Gateway>> DeleteGatewayAsync(CancellationToken cancellationToken, Gateway toDelete);
    }
}