using ErrorOr;
using FluentAssertions;
using SensorFlow.Domain.Entities.Dashboards;

namespace SensorFlow.Domain.Tests.Entities
{
    public class DashboardTests
    {
        public string validDashboardName { get; set; }
        public string validDashboardId { get; set; }

        public DashboardTests() {
            validDashboardName = "My Valid Dashboard";
            validDashboardId = "e2fc5cb1-1aa5-4fe6-ba61-1a3288850366";
        }

        [Fact]
        public void GivenDashboard_WhenCreateValid_Create()
        {
            // Act
            var dashboard = Dashboard.CreateDashboard(validDashboardName, validDashboardId);

            // Assert
            Assert.Equal("My Valid Dashboard", dashboard.Value.Name);
            Assert.Equal("e2fc5cb1-1aa5-4fe6-ba61-1a3288850366", dashboard.Value.WorkspaceId);
            Assert.NotNull(dashboard.Value.Id);
        }

        [Fact]
        public void GivenDashboard_WhenUpdateNameValid_Update()
        {
            // Act
            var dashboard = Dashboard.CreateDashboard(validDashboardName, validDashboardId);
            dashboard.Value.UpdateName("My New Dashboard");

            // Assert
            Assert.Equal("My New Dashboard", dashboard.Value.Name);
        }

        [Fact]
        public void GivenDashboard_WhenUpdateNameNotValid_Error()
        {
            // Act
            var dashboard = Dashboard.CreateDashboard(validDashboardName, validDashboardId);
            var response = dashboard.Value.UpdateName("My New Dashboard&&");
            
            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public void GivenDashboard_WhenUpdateWidgetLayoutValid_Update()
        {
            // Arrange
            var validJSON = "{\"String\": \"Is a string\",  \"Number\": 76,  \"Float\": 700.50,  \"Boolean\": true}";

            // Act
            var dashboard = Dashboard.CreateDashboard(validDashboardName, validDashboardId);
            var response = dashboard.Value.UpdateWidgetLayout(validJSON);

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.Should().Be(Result.Updated);
        }

        [Fact]
        public void GivenDashboard_WhenUpdateWidgetLayoutNotValid_Error()
        {
            // Act
            var dashboard = Dashboard.CreateDashboard(validDashboardName, validDashboardId);
            var response = dashboard.Value.UpdateWidgetLayout("{ Bad JSON Data }");

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }

        [Fact]
        public void GivenDashboard_WhenUpdateGridLayoutValid_Update()
        {
            // Arrange
            var validJSON = "{\"String\": \"Is a string\",  \"Number\": 76,  \"Float\": 700.50,  \"Boolean\": true}";

            // Act
            var dashboard = Dashboard.CreateDashboard(validDashboardName, validDashboardId);
            var response = dashboard.Value.UpdateGridLayout(validJSON);

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.Should().Be(Result.Updated);
        }

        [Fact]
        public void GivenDashboard_WhenUpdateGridLayoutNotValid_Error()
        {
            // Act
            var dashboard = Dashboard.CreateDashboard(validDashboardName, validDashboardId);
            var response = dashboard.Value.UpdateGridLayout("{ Bad JSON Data }");

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
        }
    }
}
