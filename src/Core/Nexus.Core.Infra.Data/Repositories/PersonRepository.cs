using Goal.Infra.Data;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class PersonRepository(CoreDbContext context)
    : Repository<Person, string>(context), IPersonRepository
{
}
