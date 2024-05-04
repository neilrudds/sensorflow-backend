using FluentAssertions;
using SensorFlow.Application.Dashboards.Queries;
using SensorFlow.Application.Tests.Common.Fixtures;


namespace SensorFlow.Application.Tests.Dashboards
{
    public class GetDashboardsQueryTests
    {
        private readonly DashboardFixture _dashboardFixture;

        public GetDashboardsQueryTests()
        {
            _dashboardFixture = new DashboardFixture();
        }

        [Fact]
        public async Task ShouldReturnValidDashboards_WhenDashboardsExists()
        {
            // Arrange
            var cmd = new GetDashboardsQuery();

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.Count.Should().Be(10);
        }
    }
}
