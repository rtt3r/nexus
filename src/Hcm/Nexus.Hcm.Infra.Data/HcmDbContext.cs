using Microsoft.EntityFrameworkCore;

namespace Nexus.Hcm.Infra.Data;

internal sealed class HcmDbContext(DbContextOptions<HcmDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
