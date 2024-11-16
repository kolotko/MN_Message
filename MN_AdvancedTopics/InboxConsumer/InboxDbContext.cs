using MassTransit;
using Microsoft.EntityFrameworkCore;
using OutboxDto;

namespace InboxConsumer;


//nie wywołujemy migracji (robi to outbox), potrzebne tylko do konfiguracji masstransit
public class InboxDbContext : DbContext
{
    public InboxDbContext(DbContextOptions<InboxDbContext> options): base(options)
    {
    }
    
    public DbSet<TestMessage> TestMessage { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
    }
}