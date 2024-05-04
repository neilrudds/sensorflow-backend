namespace SensorFlow.Application.Common.Models;

// Base class for all entities, these properties will be included as a minimum
public abstract class EntityDTO
{
    public DateTime CreatedTimestamp { get; set; }
    public DateTime LastModifiedTimestamp { get; set; }
    public string OwnerId { get; set; }
    public string ModifiedById { get; set; }
}