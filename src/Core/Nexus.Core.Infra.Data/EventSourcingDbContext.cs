using Microsoft.EntityFrameworkCore;
using Nexus.Core.Infra.Data.Configurations.EventSourcing;
using Nexus.Core.Infra.Data.EventStore;

namespace Nexus.Core.Infra.Data;

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
