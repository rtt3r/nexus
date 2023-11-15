using Goal.Seedwork.Application.Commands;

namespace Nexus.Core.Application.Commands.Customers;

public abstract class CustomerCommand<T> : CustomerCommand, ICommand<T>
    where T : ICommandResult
{
}
