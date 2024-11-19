using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Finance.Application.Accounts.Commands;

public record UpdateAccountCommand(string Id, string Name, string Description, string Type, string FinancialInstitutionId, string Icon, decimal InitialBalance, decimal Overdraft)
    : AccountCommand<OneOf<None, AppError>>
{
}
