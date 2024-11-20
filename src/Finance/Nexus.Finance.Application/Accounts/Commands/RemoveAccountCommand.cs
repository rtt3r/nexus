using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Finance.Application.Accounts.Commands;

public record RemoveAccountCommand(string AccountId)
    : AccountCommand<OneOf<None, AppError>>
{
}