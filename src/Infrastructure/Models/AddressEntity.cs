using System;
using System.Collections.Generic;

namespace SensorFlow.Infrastructure.Models
{
    public class AddressEntity
    {
        public Guid Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}
