using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Accounts.Aggregates;

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

    public static Account CreateAccount(string userId, string name, string? description, string type, FinancialInstitution financialInstitution, decimal initialBalance, decimal overdraft)
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
            account.SetDescription(description);
        }

        return account;
    }

    public void SetDescription(string description)
        => Description = description;

    public void SetName(string name)
        => Name = name;

    public void SetType(string type)
        => Type = Enum.Parse<AccountType>(type, true);

    public void SetFinancialInstitution(FinancialInstitution financialInstitution)
        => FinancialInstitution = financialInstitution;

    public void SetInitialBalance(decimal initialBalance)
        => InitialBalance = initialBalance;

    public void SetInitialOverdraft(decimal overdraft)
        => Overdraft = overdraft;
}
