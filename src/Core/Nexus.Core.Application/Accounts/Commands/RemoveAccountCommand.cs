using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Accounts.Commands;

public record RemoveAccountCommand(string AccountId)
    : AccountCommand<OneOf<None, AppError>>
{
}