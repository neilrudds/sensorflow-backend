using ErrorOr;
using FluentAssertions;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Domain.Tests.Entities
{
    public class DeviceTests
    {
        public string validDeviceId { get; set; }

        public string validDeviceName { get; set; }

        public string validDeviceLocation { get; set; }

        public string validDeviceFields { get; set; }

        public string validWorkspaceId { get; set; }

        public string validGatewayId { get; set; }

        public DeviceTests()
        {
            validDeviceId = "1a3288850366";
            validDeviceName = "mySensor";
            validDeviceLocation = "Belfast";
             validDeviceFields = "[{\"id\": 1,\"name\": \"Temperature\",\"identifier\": \"ambTemp\",\"type\": \"Float\",\"unit\": \"degC\"},{\"id\": 2,\"name\": \"Average Current\",\"identifier\": \"avgCurr\",\"type\": \"integer\",\"unit\": \"Celcius\"},{\"id\": 3,\"name\": \"L1 Voltage\",\"identifier\": \"voltsL1\",\"type\": \"integer\",\"unit\": \"mV\"}]";
            validWorkspaceId = "58910a68-2573-467d-ae3f-76533d46cfa4";
            validGatewayId = "0ed717df-75d3-4155-b17b-59c42f77a539";
        }

        [Fact]
        public void GivenDevice_WhenCreateValid_Create()
        {
            // Act
            var device = Device.CreateDevice(validDeviceId, validDeviceName, validDeviceLocation, validDeviceFields, validWorkspaceId, validGatewayId);

            // Assert
            Assert.Equal("1a3288850366", device.Value.Id);
            Assert.Equal("mySensor", device.Value.Name);
            Assert.Equal(validDeviceFields, device.Value.Fields);
            Assert.Equal("58910a68-2573-467d-ae3f-76533d46cfa4", device.Value.WorkspaceId);
            Assert.Equal("0ed717df-75d3-4155-b17b-59c42f77a539", device.Value.GatewayId);

        }

        [Fact]
        public void GivenDevice_WhenUpdateNameValid_Update()
        {
            // Act
            var device = Device.CreateDevice(validDeviceId, validDeviceName, validDeviceLocation, validDeviceFields, validWorkspaceId, validGatewayId);
            var response = device.Value.UpdateDeviceName("My New Device");

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.Should().Be(Result.Updated);
            Assert.Equal("My New Device", device.Value.Name);
        }

        [Fact]
        public void GivenDevice_WhenUpdateNameNotValid_Error()
        {
            // Act
            var device = Device.CreateDevice(validDeviceId, validDeviceName, validDeviceLocation, validDeviceFields, validWorkspaceId, validGatewayId);
            var response = device.Value.UpdateDeviceName("My New Device&&");

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
            Assert.Equal(validDeviceName, device.Value.Name);
        }

        [Fact]
        public void GivenDevice_WhenUpdateGatewayIdValid_Update()
        {
            // Act
            var device = Device.CreateDevice(validDeviceId, validDeviceName, validDeviceLocation, validDeviceFields, validWorkspaceId, validGatewayId);
            device.Value.UpdateDeviceGatewayId("1f46be21-00e7-4325-a210-ec62c37cf50e");

            // Assert
            Assert.Equal("1f46be21-00e7-4325-a210-ec62c37cf50e", device.Value.GatewayId);
        }

        [Fact]
        public void GivenDevice_WhenUpdateGatewayNotValid_Trim()
        {
            // Act
            var device = Device.CreateDevice(validDeviceId, validDeviceName, validDeviceLocation, validDeviceFields, validWorkspaceId, validGatewayId);
            device.Value.UpdateDeviceGatewayId("1f46be21-00e7-4325-a210-ec62c37cf50e                                           ");

            // Assert
            Assert.Equal("1f46be21-00e7-4325-a210-ec62c37cf50e", device.Value.GatewayId);
        }

        [Fact]
        public void GivenDevice_WhenUpdateFieldsValid_Update()
        {
            // Arrange
            var validUpdateFields = "[{\"id\": 1,\"name\": \"Pressure\",\"identifier\": \"press\",\"type\": \"Float\",\"unit\": \"degC\"},{\"id\": 2,\"name\": \"Average Current\",\"identifier\": \"avgCurr\",\"type\": \"integer\",\"unit\": \"Celcius\"},{\"id\": 3,\"name\": \"L1 Voltage\",\"identifier\": \"voltsL1\",\"type\": \"integer\",\"unit\": \"mV\"}]";

            // Act
            var device = Device.CreateDevice(validDeviceId, validDeviceName, validDeviceLocation, validDeviceFields, validWorkspaceId, validGatewayId);
            var response = device.Value.UpdateFields(validUpdateFields);

            // Assert
            response.IsError.Should().BeFalse();
            response.Value.Should().Be(Result.Updated);
            Assert.Equal(validUpdateFields, device.Value.Fields);
        }

        [Fact]
        public void GivenDevice_WhenUpdateFieldsNotValid_Error()
        {
            // Act
            var device = Device.CreateDevice(validDeviceId, validDeviceName, validDeviceLocation, validDeviceFields, validWorkspaceId, validGatewayId);
            var response = device.Value.UpdateFields("{ Bad JSON Data }");

            // Assert
            response.IsError.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
            Assert.Equal(validDeviceFields, device.Value.Fields);
        }
    }
}