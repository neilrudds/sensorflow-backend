using FluentAssertions;
using SensorFlow.Application.Dashboards.Queries;
using SensorFlow.Application.Tests.Common.Fixtures;

namespace SensorFlow.Application.Tests.Dashboards
{
    public class GetWorkspaceDashboardsQueryTests
    {
        private readonly DashboardFixture _dashboardFixture;

        public GetWorkspaceDashboardsQueryTests()
        {
            _dashboardFixture = new DashboardFixture();
        }

        [Fact]
        public async Task ShouldReturnNotFound_WhenWorkspaceIdDoesNotExist()
        {
            // Arrange
            var cmd = new GetWorkspaceDashboardsQuery("fc966bd8-bae2-418a-8617-4a2080644a35");

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnValidDashboard_WhenDashboardExists()
        {
            // Arrange
            var cmd = new GetWorkspaceDashboardsQuery("fc966bd8-bae2-418a-8617-4a2080644a75");

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.Count.Should().Be(10);
        }
    }
}
