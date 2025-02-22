using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Persons.Commands;

public record UpdatePersonCommand(string PersonId, string Name, string? Description, string Type, string FinancialInstitutionId, string Icon, decimal InitialBalance, decimal Overdraft)
    : PersonCommand<OneOf<None, AppError>>
{
}
