using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Accounts.Commands;

public record UpdateAccountCommand(string AccountId, string Name, string? Description, string Type, string FinancialInstitutionId, string Icon, decimal InitialBalance, decimal Overdraft)
    : AccountCommand<OneOf<None, AppError>>
{
}
