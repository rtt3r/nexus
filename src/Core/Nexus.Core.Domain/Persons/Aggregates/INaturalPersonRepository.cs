using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public interface INaturalPersonRepository : IRepository<NaturalPerson>
{
    Task<NaturalPerson?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
