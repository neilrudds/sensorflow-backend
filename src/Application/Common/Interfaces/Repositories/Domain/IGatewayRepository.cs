using ErrorOr;
using SensorFlow.Domain.Entities.Gateways;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
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