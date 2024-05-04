using ErrorOr;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Domain.Tests.Entities
{
    public class GatewayTests
    {
        public string validGatewayName { get; set; }

        public string validWorkspaceId { get; set; }

        public string validHost { get; set; }

        public GatewayTests()
        {
            validGatewayName = "My Remote Server";
            validWorkspaceId = "58910a68-2573-467d-ae3f-76533d46cfa4";
            validHost = "my.server.local";
        }

        [Fact]
        public void GivenGateway_WhenCreateValid_Create()
        {
            // Act
            var gateway = Gateway.CreateGateway(validGatewayName, validWorkspaceId, validHost);

            // Assert
            Assert.Equal("My Remote Server", gateway.Name);
            Assert.Equal("58910a68-2573-467d-ae3f-76533d46cfa4", gateway.WorkspaceId);
            Assert.Equal("my.server.local", gateway.Host);
        }

        [Fact]
        public void GivenGateway_WhenUpdateNameValid_Update()
        {
            // Act
            var gateway = Gateway.CreateGateway(validGatewayName, validWorkspaceId, validHost);
            gateway.UpdateGatewayName("My New Gateway Name");

            // Assert
            Assert.Equal("My New Gateway Name", gateway.Name);
        }

        [Fact]
        public void GivenGateway_WhenUpdateNameNotValid_Trim()
        {
            // Act
            var gateway = Gateway.CreateGateway(validGatewayName, validWorkspaceId, validHost);
            gateway.UpdateGatewayName("My New Gateway Name                                  ");

            // Assert
            Assert.Equal("My New Gateway Name", gateway.Name);
            Assert.Equal(19, gateway.Name.Length);
        }

        [Fact]
        public void GivenGateway_WhenCreateGatewayWithPortValid_Create()
        {
            // Act
            var gateway = Gateway.CreateGateway(validGatewayName, validWorkspaceId, validHost, 3000, true);

            // Assert
            Assert.Equal(3000, gateway.PortNumber);
        }

        [Fact]
        public void GivenGateway_WhenCreateGatewayPortNotValid_SetDefault()
        {
            // Act
            var gateway = Gateway.CreateGateway(validGatewayName, validWorkspaceId, validHost, 65001, true);

            // Assert
            Assert.Equal(8000, gateway.PortNumber);
        }
    }
}
