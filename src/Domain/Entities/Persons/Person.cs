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

        private Person(Guid personId, string name, string email, string phone, DateTime addedTime, DateTime lastModified)
        {
            Id = personId;
            Name = name;
            Email = email;
            Phone = phone;
            AddedTime = addedTime;
            LastModified = lastModified;
        }

        public static Person CreatePerson(Guid personId, string name, string email, string phone, DateTime addedTime, DateTime lastModified)
        {
            // Add validation..
            var person = new Person(personId, ValidateName(name), ValidateEmail(email), ValidatePhone(phone), addedTime, lastModified);
            // Do you need a domain event here?
            return person;
        }

        public void Update(string name, string email, string phone, DateTime lastModified)
        {
            Name = ValidateName(name);
            Email = ValidateEmail(email);
            Phone = ValidatePhone(phone);
            LastModified = lastModified;
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