using Goal.Infra.Data.Query;
using Nexus.Core.Model.Persons;

namespace Nexus.Core.Infra.Data.Query.Repositories.Persons;

public interface IPersonQueryRepository : IQueryRepository<NaturalPerson, string>
{
}
