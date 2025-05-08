using Goal.Domain.Aggregates;

namespace Nexus.Hcm.Domain.People.Aggregates;

public interface INaturalPersonRepository : IRepository<NaturalPerson>
{
    Task<NaturalPerson?> GetByCpf(string cpf, CancellationToken cancellationToken);
}
