using Nexus.Finance.Model.Accounts;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;

namespace Nexus.Finance.Application.Accounts.Commands;

public record RegisterAccountCommand(string Name, string Description, string Type, string FinancialInstitutionId, string Icon, decimal InitialBalance, decimal Overdraft)
    : AccountCommand<OneOf<Account, AppError>>
{
}
