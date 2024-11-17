using Microsoft.EntityFrameworkCore;
using Nexus.Infra.Data.EventSourcing.Configurations.EventSourcing;
using Nexus.Infra.Data.EventSourcing.EventStore;

namespace Nexus.Infra.Data.EventSourcing;

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
