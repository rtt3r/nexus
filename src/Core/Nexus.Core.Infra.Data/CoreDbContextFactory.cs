using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Core.Infra.Data;

internal class CoreDbContextFactory : DesignTimeDbContextFactory<CoreDbContext>
{
    protected override CoreDbContext CreateNewInstance(DbContextOptionsBuilder<CoreDbContext> optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        return new CoreDbContext(optionsBuilder.Options);
    }
}
