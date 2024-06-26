﻿using ErrorOr;
using SensorFlow.Domain.Entities.Dashboards;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IDashboardRepository
    {
        Task<ICollection<Dashboard>> GetAllAsync(CancellationToken cancellationToken);

        Task<ErrorOr<List<Dashboard>>> GetDashboardsByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId);

        Task<ErrorOr<Dashboard>> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId);

        Task<ErrorOr<Dashboard>> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate);

        Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, Dashboard toUpdate);

        Task DeleteDashboardAsync(CancellationToken cancellationToken, Dashboard toDelete);
    }
}