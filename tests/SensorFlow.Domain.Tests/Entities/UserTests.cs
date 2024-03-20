using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.ValueObjects;

namespace SensorFlow.Domain.Tests.Entities
{
    public class UserTests
    {
        [Fact]
        public void GivenUser_WhenCreateValid_Create()
        {
            var user = User.CreateUser("joe.bloggs", "Joe", "Bloggs", "joe.bloggs@hotmail.com");
            Assert.Equal("joe.bloggs", user.UserName);
            Assert.Equal("Joe", user.FirstName);
            Assert.Equal("Bloggs", user.LastName);
            Assert.Equal("joe.bloggs@hotmail.com", user.Email);
            Assert.NotNull(user.Id);
        }
    }
}