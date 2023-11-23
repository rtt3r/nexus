using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data.Configurations.Users;

namespace Nexus.Core.Infra.Data;

public abstract class CoreDbContext : DbContext
{
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected CoreDbContext(DbContextOptions options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserAccountsConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfilesConfiguration());
    }
}
