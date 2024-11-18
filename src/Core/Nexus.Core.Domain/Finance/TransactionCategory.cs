using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Finance;

public class TransactionCategory : Entity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public IList<Transaction> Transactions { get; private set; } = [];
    public IList<TransactionSubCategory> SubCategories { get; private set; } = [];

    protected TransactionCategory()
        : base()
    {
    }
}

