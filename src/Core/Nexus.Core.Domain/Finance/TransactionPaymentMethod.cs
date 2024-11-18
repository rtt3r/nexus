using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Finance;

public class TransactionPaymentMethod : Entity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public IList<Transaction> Transactions { get; private set; } = [];

    protected TransactionPaymentMethod()
        : base()
    {
    }
}
