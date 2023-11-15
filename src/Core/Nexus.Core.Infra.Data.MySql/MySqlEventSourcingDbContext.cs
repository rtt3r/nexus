using Nexus.Core.Infra.Data.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.MySql;

public class MySqlEventSourcingDbContext : EventSourcingDbContext
{
    public MySqlEventSourcingDbContext(DbContextOptions options)
        : base(options)
    { }
}
