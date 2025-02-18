using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Transactions.Aggregates;

public interface ITransactionPaymentMethodRepository : IRepository<TransactionPaymentMethod, string>
{
}
