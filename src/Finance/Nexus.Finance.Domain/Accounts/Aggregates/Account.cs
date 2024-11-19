using Goal.Domain.Aggregates;

namespace Nexus.Finance.Domain.Accounts.Aggregates;

public class Account : Entity
{
    public string UserId { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public AccountType Type { get; private set; }
    public string FinancialInstitutionId { get; private set; } = default!;
    public decimal InitialBalance { get; private set; }
    public decimal Overdraft { get; private set; }
    public FinancialInstitution FinancialInstitution { get; private set; } = default!;

    protected Account()
        : base()
    {
    }

    internal Account(string userId, string name, AccountType type, FinancialInstitution financialInstitution, decimal initialBalance, decimal overdraft)
    {
        UserId = userId;
        Name = name;
        Type = type;
        FinancialInstitution = financialInstitution;
        FinancialInstitutionId = financialInstitution.Id;
        InitialBalance = initialBalance;
        Overdraft = overdraft;
    }

    public static Account CreateAccount(string userId, string name, string description, string type, FinancialInstitution financialInstitution, decimal initialBalance, decimal overdraft)
    {
        var account = new Account(
            userId,
            name,
            Enum.Parse<AccountType>(type, true),
            financialInstitution,
            initialBalance,
            overdraft
        );

        if (!string.IsNullOrWhiteSpace(description))
        {
            account.Describe(description);
        }

        return account;
    }

    internal void Describe(string description)
        => Description = description;
}
