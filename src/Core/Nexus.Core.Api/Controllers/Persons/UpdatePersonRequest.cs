using Nexus.Core.Application.Persons.Commands;

namespace Nexus.Core.Api.Controllers.Persons;

public class UpdatePersonRequest(
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

    public UpdatePersonCommand ToCommand(string id)
    {
        return new UpdatePersonCommand(
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
