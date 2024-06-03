using Goal.Application.Commands;

namespace Nexus.Core.Application.Commands.Customers;

public record UpdateCustomerCommand(string? CustomerId, string? Name, string? Email, DateOnly? Birthdate)
    : CustomerCommand<ICommandResult>
{
}
