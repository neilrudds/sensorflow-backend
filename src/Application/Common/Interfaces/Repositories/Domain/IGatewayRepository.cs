using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IGatewayRepository
    {
        Task<(Result result, Gateway gateway)> GetGatewayByIdAsync(CancellationToken cancellationToken, string gatewayId);

        Task<(Result result, List<Gateway> gateways)> GetGatewaysByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId);

        Task<(Result result, Gateway device)> AddGatewayAsync(CancellationToken cancellationToken, Gateway toCreate);

        Task<(Result result, Gateway device)> UpdateGatewayAsync(CancellationToken cancellationToken, Gateway toUpdate);

        Task<int> DeleteGatewayAsync(CancellationToken cancellationToken, Gateway toDelete);
    }
}
