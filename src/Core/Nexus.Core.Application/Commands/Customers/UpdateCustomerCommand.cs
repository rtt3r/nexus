using Goal.Application.Commands;

namespace Nexus.Core.Application.Commands.Customers;

public record UpdateCustomerCommand(string CustomerId, string Name, DateTime Birthdate)
    : CustomerCommand<ICommandResult>
{
}
