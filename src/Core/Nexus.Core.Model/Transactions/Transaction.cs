namespace Nexus.Core.Model.Transactions;

public class Transaction
{
    public string TransactionId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateOnly Birthdate { get; set; }
}
