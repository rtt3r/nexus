using Goal.Infra.Data;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class PersonRepository(CoreDbContext context) : Repository<Person, string>(context), IPersonRepository
{
}
