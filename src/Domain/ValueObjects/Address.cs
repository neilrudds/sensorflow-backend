using SensorFlow.Domain.Models;

namespace SensorFlow.Domain.ValueObjects
{
    public class Address : Entity<string>
    {   
        public string Line1 { get; set; } = string.Empty;
        public string Line2 { get; set; } = string.Empty;   
        public string City { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty; 

        public Address() { 
            Id = Guid.NewGuid().ToString();
        }

        public Address(string line1, string line2, string city, string postCode, string country) : this()
        {
            Line1 = line1;
            Line2 = line2;
            City = city;
            Postcode = postCode;
            Country = country;
        }
    }
}