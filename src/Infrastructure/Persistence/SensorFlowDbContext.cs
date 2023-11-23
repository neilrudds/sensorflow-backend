using System.Reflection;
using SensorFlow.Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;

namespace SensorFlow.Infrastructure.Persistence;

/* Define our DBContext */

public class SensorFlowDbContext : DbContext
{
    public SensorFlowDbContext() {}
    public SensorFlowDbContext(DbContextOptions<SensorFlowDbContext> options) : base(options) {}

    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=sensorflow;User=sa;Password=Sensorflow123;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}