using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Hcm.Infra.Data;

internal class HcmDbContextFactory : DesignTimeDbContextFactory<HcmDbContext>
{
    protected override HcmDbContext CreateNewInstance(DbContextOptionsBuilder<HcmDbContext> optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        return new HcmDbContext(optionsBuilder.Options);
    }
}
