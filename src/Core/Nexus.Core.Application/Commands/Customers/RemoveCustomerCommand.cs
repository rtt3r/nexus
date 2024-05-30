using Goal.Application.Commands;

namespace Nexus.Core.Application.Commands.Customers;

public record RemoveCustomerCommand(string? CustomerId)
    : CustomerCommand<ICommandResult>
{
}
