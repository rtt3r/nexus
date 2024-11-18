using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Finance;

public class Transaction : Entity
{
    public TransactionType Type { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public string CategoryId { get; private set; } = null!;
    public string SubCategoryId { get; private set; } = null!;
    public string PaymentMethodId { get; private set; } = null!;
    public DateOnly Date { get; private set; }
    public bool Completed { get; private set; }
    public DateTimeOffset StoredAt { get; private set; }
    public string UserId { get; private set; } = null!;
    public TransactionCategory Category { get; private set; } = null!;
    public TransactionSubCategory SubCategory { get; private set; } = null!;
    public TransactionPaymentMethod PaymentMethod { get; private set; } = null!;

    protected Transaction()
        : base()
    {
    }

    public Transaction(
        TransactionType type,
        string name,
        decimal amount,
        TransactionCategory category,
        TransactionSubCategory subCategory,
        TransactionPaymentMethod paymentMethod,
        DateOnly date,
        bool completed,
        string userId)
        : base()
    {
        Name = name;
        Type = type;
        Amount = amount;
        Category = category;
        PaymentMethod = paymentMethod;
        Date = date;
        StoredAt = DateTime.UtcNow;
        UserId = userId;
    }
}
