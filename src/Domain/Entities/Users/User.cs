using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SensorFlow.Domain.Entities.Workspaces;

// To-Do; Needs Validation

namespace SensorFlow.Domain.Entities.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address? Address { get; set; }
        public bool IsActive { get; set; } = false;
        public string TenantId { get; set; }
        public Tenant Tenant { get; set; }

        // Navigation Properties
        public virtual ICollection<UserRole> Roles { get; } = new List<UserRole>();

        public ICollection<Workspace> Workspaces { get; set; }

        public User() 
        {
            Id = Guid.NewGuid().ToString();
        }

        public User(string userName, string firstName, string lastName, string email) : this()
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public User(string userName, string firstName, string lastName, string email, string tenantId) : this()
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TenantId = tenantId;
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
