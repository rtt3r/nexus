using Microsoft.EntityFrameworkCore;
using Nexus.Core.Infra.Data.EventSourcing;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlEventSourcingDbContext(DbContextOptions options) : EventSourcingDbContext(options)
{
}
