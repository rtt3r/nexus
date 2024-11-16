using Microsoft.EntityFrameworkCore;
using Nexus.Infra.EventSourcing.Configurations.EventSourcing;
using Nexus.Infra.EventSourcing.EventStore;

namespace Nexus.Infra.EventSourcing;

internal sealed class EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options)
    : DbContext(options)
{
    public DbSet<StoredEvent> StoredEvents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new StoredEventConfiguration());
    }
}
