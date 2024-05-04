using SensorFlow.Domain.Entities.Users;

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

        [Fact]
        public void GivenUserAddress_WhenChangeValid_Change()
        {
            var user = User.CreateUser("joe.bloggs", "Joe", "Bloggs", "joe.bloggs@hotmail.com");
            user.ChangeAddress("Queens University Belfast", "University Rd", "Belfast", "BT7 1NN", "NI");
            Assert.Equal("Queens University Belfast", user.Address.Line1);
            Assert.Equal("University Rd", user.Address.Line2);
            Assert.Equal("Belfast", user.Address.City);
            Assert.Equal("BT7 1NN", user.Address.Postcode);
            Assert.Equal("NI", user.Address.Country);
            Assert.NotNull(user.Id);
        }

        [Fact]
        public void GivenUserDeactivated_WhenActivateValid_Activate()
        {
            var user = User.CreateUser("joe.bloggs", "Joe", "Bloggs", "joe.bloggs@hotmail.com");
            user.Activate();
            Assert.True(user.IsActive);
        }

        [Fact]
        public void GivenUserActivated_WhenDeactivateValid_Deactivate()
        {
            var user = User.CreateUser("joe.bloggs", "Joe", "Bloggs", "joe.bloggs@hotmail.com");
            user.Activate();
            Assert.True(user.IsActive);
            user.Deactivate();
            Assert.False(user.IsActive);
        }
    }
}