using Goal.Infra.Data.Query;
using Nexus.Finance.Model.Accounts;

namespace Nexus.Finance.Infra.Data.Query.Repositories.Accounts;

public interface IAccountQueryRepository : IQueryRepository<Account, string>
{
}
