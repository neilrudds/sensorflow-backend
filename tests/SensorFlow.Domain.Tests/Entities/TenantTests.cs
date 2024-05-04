using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Workspaces;

namespace SensorFlow.Domain.Tests.Entities
{
    public class TenantTests
    {
        [Fact]
        public void GivenTenant_WhenCreateValid_Create()
        {
            var tenant = Tenant.CreateTenant("General Automation Ltd.");
            Assert.Equal("General Automation Ltd.", tenant.Name);
            Assert.NotNull(tenant.Id);
        }

        [Fact]
        public void GivenTenant_WhenCreateNameHasWhitespace_Trim()
        {
            var tenant = Tenant.CreateTenant("General Automation Ltd.            ");
            Assert.Equal("General Automation Ltd.", tenant.Name);
            Assert.Equal(23, tenant.Name.Length);
            Assert.NotNull(tenant.Id);
        }
    }
}
