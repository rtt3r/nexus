using Nexus.Core.Infra.Data.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlEventSourcingDbContext : EventSourcingDbContext
{
    public NpgsqlEventSourcingDbContext(DbContextOptions options)
        : base(options)
    { }
}
