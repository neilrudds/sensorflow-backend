using FluentAssertions;
using SensorFlow.Application.Dashboards.Commands;
using SensorFlow.Application.Tests.Common.Fixtures;

namespace SensorFlow.Application.Tests.Dashboards
{
    public class UpdateDashboardCommandTests
    {
        private readonly DashboardFixture _dashboardFixture;

        public UpdateDashboardCommandTests()
        {
            _dashboardFixture = new DashboardFixture();
        }

        [Fact]
        public async Task ShouldReturnNotFound_WhenDashboardIdDoesNotExist()
        {
            // Arrange
            var cmd = new UpdateDashboardCommand(Guid.NewGuid().ToString(), "{}", "{}");

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnValidationError_WhenGridWidgetsFormatIsInvalid()
        {
            // Arrange
            var cmd = new UpdateDashboardCommand("1", "{ Bad JSON Data }", "{}");

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnValidationError_WhenGridLayoutFormatIsInvalid()
        {
            // Arrange
            var cmd = new UpdateDashboardCommand("1", "{}", "{ Bad JSON Data }");

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnValidResponse_WhenAllDataIsValid()
        {
            // Arrange
            var validJSON = "{\"String\": \"Is a string\",  \"Number\": 76,  \"Float\": 700.50,  \"Boolean\": true}";
            var cmd = new UpdateDashboardCommand("1", validJSON, validJSON);

            // Act
            var response = await _dashboardFixture.Send(cmd);

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.GridWidgets.Should().Be(validJSON);
            response.Value.GridLayout.Should().Be(validJSON);   
        }
    }
}
