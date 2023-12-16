using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Persons.Models
{
    public sealed class PersonDTO : EntityDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}