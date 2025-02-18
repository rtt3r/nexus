using Goal.Infra.Data;
using Nexus.Core.Domain.Accounts.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class FinancialInstitutionRepository(CoreDbContext context)
    : Repository<FinancialInstitution, string>(context), IFinancialInstitutionRepository
{
}
