using Nexus.Core.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data;

public abstract class CoreDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected CoreDbContext(DbContextOptions options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}
