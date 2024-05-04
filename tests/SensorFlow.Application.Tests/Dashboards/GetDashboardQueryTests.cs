using FluentAssertions;
using SensorFlow.Application.Dashboards.Queries;
using SensorFlow.Application.Tests.Common.Fixtures;


namespace SensorFlow.Application.Tests.Dashboards
{
    public class GetDashboardQueryTests
    {
        private readonly DashboardFixture _dashboardFixture;

        public GetDashboardQueryTests()
        {
            _dashboardFixture = new DashboardFixture();
        }

        [Fact]
        public async Task ShouldReturnNotFound_WhenDashboardDoesNotExist()
        {
            // Arrange
            var cmd = new GetDashboardQuery("fc966bd8-bae2-418a-8617-4a2080644a35");

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
            var cmd = new GetDashboardQuery("fc966bd8-bae2-418a-8617-4a2080644a99");

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.name.Should().Be("New Dashboard II");
            response.Value.workspaceId.Should().Be("5000");
            response.Value.CreatedTimestamp.Should().BeAfter(DateTime.UtcNow.AddMinutes(-10));
        }
    }
}
