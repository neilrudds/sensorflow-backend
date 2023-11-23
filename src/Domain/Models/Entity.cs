namespace SensorFlow.Domain.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }
    
    public DateTime AddedTime { get; set; }
    public DateTime LastModified { get; set; }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }
    
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}