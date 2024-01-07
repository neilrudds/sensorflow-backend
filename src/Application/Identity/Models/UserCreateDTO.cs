namespace SensorFlow.Application.Identity.Models
{
	public sealed class UserCreateDTO
	{
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string? tenantId { get; set; }
        public string password { get; set; }
    }
}