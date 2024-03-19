using SensorFlow.Domain.Entities.Gateways;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;
using System.Text.Json.Nodes;

namespace SensorFlow.Domain.Entities.Devices
{
    public sealed class Device : Entity<string>
    {
        public string Name { get; set; } = string.Empty;

        public string? Fields { get; set; } = string.Empty;

        // Navigation Property
        public string WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }

        // Navigation Property
        public string GatewayId { get; set; }
        public Gateway Gateway { get; set; }

        public Device() { 
            //Id = Guid.NewGuid().ToString(); // DeviceId is generated in the front-end
            //Workspaces = new Collection<Workspace>();
        }

        public Device(string id, string name, string fields, string workspaceId, string gatewayId) : this()
        {
            Id = id;
            Name = name;
            Fields = fields;
            WorkspaceId = workspaceId;
            GatewayId = gatewayId;
        }

        public static Device CreateDevice(string id, string name, string fields, string workspaceId, string gatewayId)
        {
            // Do I need validation on workspaceId?
            var device = new Device(ValidateId(id), ValidateName(name), ValidateJSON(fields), workspaceId, gatewayId);
            return device;
        }

        public void UpdateDeviceName(string name)
        {
            Name = ValidateName(name);
        }

        public void UpdateDeviceGatewayId(string gatewayId)
        {
            GatewayId = ValidateId(gatewayId);
        }

        public void UpdateFields(string fields)
        {
            Fields = ValidateJSON(fields);
        }

        private static string ValidateId(string? id)
        {
            id = (id ?? string.Empty).Trim();
            return id;
        }

        private static string ValidateName(string? name)
        {
            name = (name ?? string.Empty).Trim();
            return name;
        }

        private static string ValidateJSON(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return string.Empty;

            json = json.Trim();
            if ((json.StartsWith("{") && json.EndsWith("}")) || //For object
                (json.StartsWith("[") && json.EndsWith("]"))) //For array
            {
                try
                {
                    var tmpObj = JsonValue.Parse(json);
                    return json;
                }
                catch (FormatException ex)
                {
                    //Invalid json format
                    return string.Empty;
                }
                catch (Exception ex) //some other exception
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
