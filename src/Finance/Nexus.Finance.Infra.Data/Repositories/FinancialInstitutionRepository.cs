using Goal.Infra.Data;
using Nexus.Finance.Domain.Accounts.Aggregates;

namespace Nexus.Finance.Infra.Data.Repositories;

internal sealed class FinancialInstitutionRepository(FinanceDbContext context)
    : Repository<FinancialInstitution, string>(context), IFinancialInstitutionRepository
{
}
