using Nexus.Core.Application.Accounts.Commands;

namespace Nexus.Core.Api.Controllers.Accounts;

public class UpdateAccountRequest(
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

    public UpdateAccountCommand ToCommand(string id)
    {
        return new UpdateAccountCommand(
            id,
            Name,
            Description,
            Type,
            FinancialInstitutionId,
            Icon,
            InitialBalance,
            Overdraft);
    }
}
