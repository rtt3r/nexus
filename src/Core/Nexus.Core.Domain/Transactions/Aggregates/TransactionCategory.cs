using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Transactions.Aggregates;

public class TransactionCategory : Entity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public IList<Transaction> Transactions { get; private set; } = [];
    public IList<TransactionSubCategory> SubCategories { get; private set; } = [];

    protected TransactionCategory()
        : base()
    {
    }
}

