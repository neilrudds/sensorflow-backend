﻿using MediatR;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using ErrorOr;

namespace SensorFlow.Application.Dashboards.Commands
{
    // Command
    public record CreateDashboardCommand(string name, string workspaceId) : IRequest<ErrorOr<Dashboard>>;

    // Command Handler
    public class CreateDashboardCommandHandler : IRequestHandler<CreateDashboardCommand, ErrorOr<Dashboard>>
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateDashboardCommandHandler(IDashboardRepository dashboardRepository, IWorkspaceRepository workspaceRepository)
        {
            _dashboardRepository = dashboardRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<ErrorOr<Dashboard>> Handle(CreateDashboardCommand request, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            if (workspace is null)
                return Error.NotFound(description: "Workspace not found");

            var dashboard = Dashboard.CreateDashboard(
                request.name,
                request.workspaceId
            );

            var result = await _dashboardRepository.AddDashboardAsync(cancellationToken, dashboard);

            if (result.IsError)
                return result.Errors;

            return dashboard;
        }
    }
}