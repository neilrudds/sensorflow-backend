using SensorFlow.Domain.Entities.Workspaces;

namespace SensorFlow.Domain.Tests.Entities
{
    public class WorkspaceTests
    {
        [Fact]
        public void GivenWorkspace_WhenCreateValid_Create()
        {
            var workspace = Workspace.CreateWorkspace("Test Workspace", "cba0541b-d850-4d91-b021-74d732ac16f6");
            Assert.NotNull(workspace);
            Assert.Equal("Test Workspace", workspace.Name);
            Assert.Equal("cba0541b-d850-4d91-b021-74d732ac16f6", workspace.TenantId);
            Assert.NotNull(workspace.Id);
        }

        [Fact]
        public void GivenWorkspace_WhenCreateNameHasWhitespace_Trim()
        {
            var workspace = Workspace.CreateWorkspace("Test Workspace      ", "cba0541b-d850-4d91-b021-74d732ac16f6");
            Assert.NotNull(workspace);
            Assert.Equal("Test Workspace", workspace.Name);
            Assert.Equal(14, workspace.Name.Length);
            Assert.Equal("cba0541b-d850-4d91-b021-74d732ac16f6", workspace.TenantId);
            Assert.NotNull(workspace.Id);
        }
    }
}
