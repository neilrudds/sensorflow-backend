using FluentAssertions;
using SensorFlow.Application.Dashboards.Commands;
using SensorFlow.Application.Tests.Common.Fixtures;

namespace SensorFlow.Application.Tests.Dashboards
{
    public class CreateDashboardCommandTests
    {
        private readonly DashboardFixture _dashboardFixture;

        public CreateDashboardCommandTests() 
        {
            _dashboardFixture = new DashboardFixture();
        }

        [Fact]
        public async Task ShouldReturnNotFound_WhenWorkspaceIdDoesNotExist()
        {
            // Arrange
            var cmd = new CreateDashboardCommand("My New Dashboard", Guid.NewGuid().ToString());

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnValidationError_WhenWorkspaceIdExists()
        {
            // Arrange
            var dashboardName = "My New Dashboard";
            var cmd = new CreateDashboardCommand(dashboardName, "1");

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.Id.Should().NotBeNullOrWhiteSpace();
            response.Value.Name.Should().Be(dashboardName);
            response.Value.WorkspaceId.Should().NotBeNullOrWhiteSpace();
        }
    }
}
