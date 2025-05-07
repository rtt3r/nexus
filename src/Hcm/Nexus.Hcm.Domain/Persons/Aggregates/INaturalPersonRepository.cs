using Goal.Domain.Aggregates;

namespace Nexus.Hcm.Domain.Persons.Aggregates;

public interface INaturalPersonRepository : IRepository<NaturalPerson>
{
    Task<NaturalPerson?> GetByCpf(string cpf, CancellationToken cancellationToken);
}
