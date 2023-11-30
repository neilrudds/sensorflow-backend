// Extend the default identity user class
// I dont think this should be in the Application project - Check Clean Architecture principles.
using Microsoft.AspNetCore.Identity;

namespace SensorFlow.Infrastructure.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressEntity? Address { get; set; }
        public bool IsActive { get; set; } = false;
        public virtual ICollection<ApplicationUserRole> Roles { get; } = new List<ApplicationUserRole>();
    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}