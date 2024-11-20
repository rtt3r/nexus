using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Accounts.Aggregates;

public interface IAccountRepository : IRepository<Account, string>
{
    Task<Account?> GetFromUserAsync(string userId, string accountId, CancellationToken cancellationToken = default);
    Task<Account?> GetFromUserByName(string userId, string name, CancellationToken cancellationToken = default);
}
