using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Transactions.Aggregates;

public interface ITransactionRepository : IRepository<Transaction, string>
{
}
