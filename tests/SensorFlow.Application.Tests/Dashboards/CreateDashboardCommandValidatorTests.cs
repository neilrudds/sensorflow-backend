using FluentAssertions;
using SensorFlow.Application.Dashboards.Commands;


namespace SensorFlow.Application.Tests.Dashboards
{
    public class CreateDashboardCommandValidatorTests
    {
        [Fact]
        public async Task ShouldReturnValidationError_WhenDashboardNameIsTooShort()
        {
            // Arrange
            var validator = new CreateDashboardCommandValidator();
            var cmd = new CreateDashboardCommand("DB", Guid.NewGuid().ToString());

            // Act
            var response = await validator.ValidateAsync(cmd);

            // Assert
            response.IsValid.Should().BeFalse();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnValidationError_WhenDashboardNameIsEmpty()
        {
            // Arrange
            var validator = new CreateDashboardCommandValidator();
            var cmd = new CreateDashboardCommand("", Guid.NewGuid().ToString());

            // Act
            var response = await validator.ValidateAsync(cmd);

            // Assert
            response.IsValid.Should().BeFalse();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnValidationError_WhenWorkspaceIdIsEmpty()
        {
            // Arrange
            var validator = new CreateDashboardCommandValidator();
            var cmd = new CreateDashboardCommand("My Dashboard", "");

            // Act
            var response = await validator.ValidateAsync(cmd);

            // Assert
            response.IsValid.Should().BeFalse();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldReturnResponse_WhenDashboardCommandIsValid()
        {
            // Arrange
            var validator = new CreateDashboardCommandValidator();
            var cmd = new CreateDashboardCommand("My Dashboard", "1");

            // Act
            var response = await validator.ValidateAsync(cmd);

            // Assert
            response.IsValid.Should().BeTrue();
            response.Errors.Should().HaveCount(0);
        }
    }
}
