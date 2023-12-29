using SensorFlow.Domain.Models;

/* Person Model for data */
namespace SensorFlow.Domain.Entities.Persons
{
    public sealed class Person : Entity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        private Person()
        {

        }

        private Person(Guid personId, string name, string email, string phone)
        {
            Id = personId;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public static Person CreatePerson(Guid personId, string name, string email, string phone)
        {
            // Add validation..
            var person = new Person(personId, ValidateName(name), ValidateEmail(email), ValidatePhone(phone));
            // Do you need a domain event here?
            return person;
        }

        public void Update(string name, string email, string phone)
        {
            Name = ValidateName(name);
            Email = ValidateEmail(email);
            Phone = ValidatePhone(phone);
        }

        private static string ValidateName(string? name)
        {
            // Some form of validation
            name = (name ?? string.Empty).Trim();
            return name;
        }

        private static string ValidateEmail(string? email)
        {
            // Some form of validation
            email = (email ?? string.Empty).Trim();
            return email;
        }

        private static string ValidatePhone(string? phone)
        {
            // Some form of validation
            phone = (phone ?? string.Empty).Trim();
            return phone;
        }
    }
}