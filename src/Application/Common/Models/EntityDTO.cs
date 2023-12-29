namespace SensorFlow.Application.Common.Models;
public abstract class EntityDTO
{
    public DateTime CreatedTimestamp { get; set; }
    public DateTime LastModifiedTimestamp { get; set; }
    public string OwnerId { get; set; }
    public string ModifiedById { get; set; }
}