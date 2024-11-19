using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Transactions.Aggregates;

public interface ITransactionPaymentMethodRepository : IRepository<TransactionPaymentMethod, string>
{
}
