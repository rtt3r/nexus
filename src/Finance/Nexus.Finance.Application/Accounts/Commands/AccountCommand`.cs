using Goal.Application.Commands;

namespace Nexus.Finance.Application.Accounts.Commands;

public record AccountCommand<T> : AccountCommand, ICommand<T>
{
}
