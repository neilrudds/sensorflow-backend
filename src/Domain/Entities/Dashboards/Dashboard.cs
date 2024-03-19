using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;
using System.Text.Json.Nodes;

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

        public static Dashboard CreateDashboard(string name, string workspaceId)
        {
            // Do I need validation on workspaceId?
            var dashboard = new Dashboard(ValidateName(name), workspaceId);
            return dashboard;
        }

        public void Update(string name, string gridLayout)
        {
            Name = ValidateName(name);
            GridLayout = ValidateJSON(gridLayout);
        }

        public void UpdateGridLayout(string gridLayout)
        {
            GridLayout = ValidateJSON(gridLayout);
        }

        public void UpdateWidgetLayout(string gridLayout)
        {
            GridWidgets = ValidateJSON(gridLayout);
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
