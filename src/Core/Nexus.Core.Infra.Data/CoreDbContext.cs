using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data.Configurations.Users;

namespace Nexus.Core.Infra.Data;

public abstract class CoreDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserAccountsConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfilesConfiguration());
    }
}
