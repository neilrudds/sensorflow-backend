using SensorFlow.Domain.Entities.Devices;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;

namespace SensorFlow.Domain.Entities.Gateways
{
    public sealed class Gateway : Entity<string>
    {
        public string Name { get; set; } = string.Empty;

        public string Host { get; set; } = string.Empty;

        public int PortNumber { get; set; } = 8000;

        public string Username {  get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ClientId {  get; set; } = string.Empty;

        public bool SSLEnabled { get; set; } = false;

        // Navigation Property
        public string WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
        public ICollection<Device> Devices { get; set; } = new List<Device>();

        public Gateway()
        {
            Id = Guid.NewGuid().ToString();
            //Workspaces = new Collection<Workspace>();
        }

        public Gateway(string name, string workspaceId, string host) : this()
        {
            Name = name;
            WorkspaceId = workspaceId;
            Host = host;
        }

        public static Gateway CreateGateway(string name, string workspaceId, string host)
        {
            // Do I need validation on workspaceId?
            var gateway = new Gateway(ValidateName(name), workspaceId, host);
            return gateway;
        }

        public static Gateway CreateGateway(string name, string workspaceId, string host, int port, bool sSLEnabled)
        {
            // Do I need validation on workspaceId?
            var gateway = new Gateway(ValidateName(name), workspaceId, host);
            gateway.SSLEnabled = sSLEnabled;
            gateway.PortNumber = ValidatePort(port);
            return gateway;
        }

        public void UpdateGatewayName(string name)
        {
            Name = ValidateName(name);
        }

        private static string ValidateName(string? name)
        {
            name = (name ?? string.Empty).Trim();
            return name;
        }

        private static int ValidatePort(int port)
        {
            if (port > 0 && port < 65000)
            {
                return port;
            }
            else
            {
                return 8000;
            }
        }
    }
}
