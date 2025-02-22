using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class NaturalPersonRepository(CoreDbContext context)
    : Repository<NaturalPerson, string>(context), INaturalPersonRepository
{
    public async Task<NaturalPerson?> GetByCpf(string cpf, CancellationToken cancellationToken)
    {
        return await Context.Set<NaturalPerson>()
            .FirstOrDefaultAsync(
                p => p.Documents.Any(d => d.Type.Name == Domains.DocumentTypes.CPF && d.Number == cpf),
                cancellationToken);
    }
}
