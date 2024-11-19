using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Transactions.Aggregates;

public class TransactionSubCategory : Entity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string CategoryId { get; private set; } = default!;
    public TransactionCategory Category { get; private set; } = default!;
    public IList<Transaction> Transactions { get; private set; } = [];

    protected TransactionSubCategory()
        : base()
    {
    }
}
