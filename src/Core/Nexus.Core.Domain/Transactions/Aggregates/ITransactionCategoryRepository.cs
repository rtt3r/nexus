using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Transactions.Aggregates;

public interface ITransactionCategoryRepository : IRepository<TransactionCategory, string>
{
}
