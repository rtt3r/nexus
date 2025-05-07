using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class NaturalPersonRepository(CoreDbContext context)
    : Repository<NaturalPerson, string>(context), INaturalPersonRepository
{
    public async Task<NaturalPerson?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        return await Context.Set<NaturalPerson>()
            .FirstOrDefaultAsync(
                c => c.Documents.Any(d => d.Document.Name == "Cpf" && d.Value == cpf),
                cancellationToken);
    }
}
