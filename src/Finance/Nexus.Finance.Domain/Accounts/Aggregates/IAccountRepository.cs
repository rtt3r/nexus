using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Accounts.Aggregates;

public interface IAccountRepository : IRepository<Account, string>
{
    Task<Account?> GetByUserAndName(string userId, string name, CancellationToken cancellationToken = default);
}
