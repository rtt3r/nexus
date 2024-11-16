using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Core.Infra.Data;

internal sealed class EventSourcingDbContextFactory : DesignTimeDbContextFactory<EventSourcingDbContext>
{
    protected override EventSourcingDbContext CreateNewInstance(DbContextOptionsBuilder<EventSourcingDbContext> optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        return new EventSourcingDbContext(optionsBuilder.Options);
    }
}
