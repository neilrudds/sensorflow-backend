namespace SensorFlow.Application.Identity.Models
{
    public sealed class UserDTO
    {
        public Guid Id { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public bool? lockedOut { get; set; }

        public bool? isActive { get; set; }
    }
}