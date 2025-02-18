using Goal.Infra.Data.Query;
using Nexus.Core.Model.Accounts;

namespace Nexus.Core.Infra.Data.Query.Repositories.Accounts;

public interface IAccountQueryRepository : IQueryRepository<Account, string>
{
}
