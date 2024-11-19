using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Accounts.Aggregates;

public interface IFinancialInstitutionRepository : IRepository<FinancialInstitution, string>
{
}
