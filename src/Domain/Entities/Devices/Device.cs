using ErrorOr;
using SensorFlow.Domain.Entities.Gateways;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

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
        }

        public Device(string id, string name, string fields, string workspaceId, string gatewayId) : this()
        {
            Id = id;
            Name = name;
            Fields = fields;
            WorkspaceId = workspaceId;
            GatewayId = gatewayId;
        }

        public static ErrorOr<Device> CreateDevice(string id, string name, string fields, string workspaceId, string gatewayId)
        {
            var _name = ValidateName(name);
            if (_name.IsError)
                return _name.Errors;

            var _fields = ValidateName(fields);
            if (_fields.IsError)
                return _fields.Errors;

            var device = new Device(ValidateId(id), _name.Value, _fields.Value, workspaceId, gatewayId);
            return device;
        }

        public ErrorOr<Updated> UpdateDeviceName(string name)
        {
            var _name = ValidateName(name);
            if (_name.IsError)
                return _name.Errors;

            Name = _name.Value;
            return Result.Updated;
        }

        public void UpdateDeviceGatewayId(string gatewayId)
        {
            GatewayId = ValidateId(gatewayId);
        }

        public ErrorOr<Updated> UpdateFields(string fields)
        {
            var _fields = ValidateJSON(fields);
            if (_fields.IsError)
                return _fields.Errors;

            Fields = _fields.Value;
            return Result.Updated;
        }

        private static string ValidateId(string? id)
        {
            id = (id ?? string.Empty).Trim();
            return id;
        }

        private static ErrorOr<string> ValidateName(string name)
        {
            Regex r = new Regex("^[a-zA-Z0-9 ]*$");
            if (r.IsMatch(name))
            {
                return name.Trim();
            }
            return Error.Validation(description: "Device name contains invalid characters");
        }

        private static ErrorOr<string> ValidateJSON(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return Error.Validation(description: "JSON data is empty");

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
                    return Error.Validation(description: "JSON format is invalid");
                }
                catch (Exception ex) //some other exception
                {
                    return Error.Unexpected(code: "JSON error", description: ex.Message);
                }
            }
            else
            {
                return Error.Validation(description: "JSON format is invalid");
            }
        }
    }
}
