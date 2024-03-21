using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using ErrorOr;

namespace SensorFlow.Domain.Entities.Dashboards
{
    public sealed class Dashboard : Entity<string>
    {
        public string Name { get; set; } = string.Empty;
        public string? GridWidgets { get; set; }
        public string? GridLayout { get; set; }
        public string WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } // Required foreign key property, required relationship as not nullable.

        public Dashboard()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Dashboard(string name, string workspaceId) : this()
        {
            Name = name;
            WorkspaceId = workspaceId;
        }

        public static ErrorOr<Dashboard> CreateDashboard(string name, string workspaceId)
        {
            var _name = ValidateName(name);
            if (_name.IsError)
                return _name.Errors;

            var dashboard = new Dashboard(_name.Value, workspaceId);
            return dashboard;
        }

        public ErrorOr<Updated> UpdateName(string name)
        {
            var _name = ValidateName(name);
            if (_name.IsError)
                return _name.Errors;

            Name = _name.Value;
            return Result.Updated;
        }

        public ErrorOr<Updated> UpdateGridLayout(string gridLayout)
        {
            var _gridLayout = ValidateJSON(gridLayout);
            if (_gridLayout.IsError)
                return _gridLayout.Errors;

            GridLayout = _gridLayout.Value;
            return Result.Updated;
        }

        public ErrorOr<Updated> UpdateWidgetLayout(string gridWidgets)
        {
            var _gridWidgets = ValidateJSON(gridWidgets);
            if (_gridWidgets.IsError)
                return _gridWidgets.Errors;

            GridWidgets = _gridWidgets.Value;
            return Result.Updated;
        }

        private static ErrorOr<string> ValidateName(string name)
        {
            Regex r = new Regex("^[a-zA-Z0-9 ]*$");
            if(r.IsMatch(name))
            {
                return name.Trim();
            }
            return Error.Validation(description: "Dashboard name contains invalid characters");
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
