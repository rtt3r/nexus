using Nexus.Finance.Application.Accounts.Commands;

namespace Nexus.Finance.Api.Controllers.Accounts;

public class RegisterAccountRequest(
    string name,
    string description,
    string type,
    string financialInstitutionId,
    string icon,
    decimal initialBalance,
    decimal overdraft)
{
    public string Name { get; } = name;
    public string? Description { get; } = description;
    public string Type { get; } = type;
    public string FinancialInstitutionId { get; } = financialInstitutionId;
    public string Icon { get; } = icon;
    public decimal InitialBalance { get; } = initialBalance;
    public decimal Overdraft { get; } = overdraft;

    public  RegisterAccountCommand ToCommand()
    {
        return new RegisterAccountCommand(
            Name,
            Description,
            Type,
            FinancialInstitutionId,
            Icon,
            InitialBalance,
            Overdraft);
    }
}
