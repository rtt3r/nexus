namespace Nexus.Core.Application.Customers.Commands;

public record UpdateCustomerCommand(string? CustomerId, string? Name, string? Email, DateOnly? Birthdate)
    : CustomerCommand
{
}
