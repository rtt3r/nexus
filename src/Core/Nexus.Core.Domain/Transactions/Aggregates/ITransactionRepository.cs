using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Transactions.Aggregates;

public interface ITransactionRepository : IRepository<Transaction, string>
{
}
