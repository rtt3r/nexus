using Microsoft.EntityFrameworkCore;
using Nexus.Core.Infra.Data.Configurations.EventSourcing;

namespace Nexus.Core.Infra.Data.EventSourcing;

public abstract class EventSourcingDbContext : DbContext
{
    public DbSet<StoredEvent> StoredEvents { get; set; }

    protected EventSourcingDbContext(DbContextOptions options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new StoredEventConfiguration());
    }
}
