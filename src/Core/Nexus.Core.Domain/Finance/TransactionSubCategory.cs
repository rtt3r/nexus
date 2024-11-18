using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Finance;

public class TransactionSubCategory : Entity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string CategoryId { get; private set; } = null!;
    public TransactionCategory Category { get; private set; } = null!;
    public IList<Transaction> Transactions { get; private set; } = [];

    protected TransactionSubCategory()
        : base()
    {
    }
}
