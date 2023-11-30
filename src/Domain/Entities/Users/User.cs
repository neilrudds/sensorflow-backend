using SensorFlow.Domain.Enumerations;
using SensorFlow.Domain.ValueObjects;

namespace SensorFlow.Domain.Entities.Users
{
    public sealed class User
    {
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }
        public string Email { get; private set; }
        public IReadOnlyCollection<RoleEnum> Roles { get; private set; }
        public bool IsActive { get; set; }

        private User() { }

        public User(string userName, string firstName, string lastName, string email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void ChangeAddress(string line1, string line2, string city, string postCode, string country)
        {
            Address = new Address(line1, line2, city, postCode, country);
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
