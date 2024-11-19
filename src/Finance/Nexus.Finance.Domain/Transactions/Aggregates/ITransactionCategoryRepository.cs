using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Transactions.Aggregates;

public interface ITransactionCategoryRepository : IRepository<TransactionCategory, string>
{
}
