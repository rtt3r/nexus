using Microsoft.EntityFrameworkCore;
using Nexus.Core.Infra.Data.Configurations.EventSourcing;

namespace Nexus.Core.Infra.Data.EventSourcing;

public abstract class EventSourcingDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<StoredEvent> StoredEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new StoredEventConfiguration());
    }
}
