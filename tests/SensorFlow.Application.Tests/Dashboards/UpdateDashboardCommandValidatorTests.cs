using FluentAssertions;
using SensorFlow.Application.Dashboards.Commands;

namespace SensorFlow.Application.Tests.Dashboards
{
    public class UpdateDashboardCommandValidatorTests
    {
        [Fact]
        public async Task ShouldReturnValidationError_WhenDashboardUpdateIsEmpty()
        {
            // Arrange
            var validator = new UpdateDashboardCommandValidator();
            var cmd = new UpdateDashboardCommand(String.Empty, "{}", "{}");

            // Act
            var response = await validator.ValidateAsync(cmd);

            // Assert
            response.IsValid.Should().BeFalse();
            response.Errors.Should().HaveCount(1);
        }
    }
}
