using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Accounts.Aggregates;

public interface IAccountRepository : IRepository<Account, string>
{
    Task<Account?> GetByName(string name, CancellationToken cancellationToken = default);
}
