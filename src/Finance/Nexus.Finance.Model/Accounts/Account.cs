namespace Nexus.Finance.Model.Accounts;

public class Account
{
    public string AccountId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Type { get; set; } = default!;
    public decimal InitialBalance { get; set; }
    public decimal Overdraft { get; set; }
    public FinancialInstitution FinancialInstitution { get; set; } = default!;
}
