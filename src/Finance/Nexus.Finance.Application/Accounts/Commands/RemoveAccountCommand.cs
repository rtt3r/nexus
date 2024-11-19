using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Finance.Application.Accounts.Commands;

public record RemoveAccountCommand(string Id)
    : AccountCommand<OneOf<None, AppError>>
{
}