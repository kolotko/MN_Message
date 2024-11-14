using Microsoft.EntityFrameworkCore;
using Orchestrator.Saga;

namespace Orchestrator.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventSagaData>().HasKey(s => s.CorrelationId);
    }

    // public DbSet<Subscriber> Subscribers { get; set; }
    //
    public DbSet<EventSagaData> SagaData { get; set; }
}