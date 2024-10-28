namespace Nexus.Core.Application.Customers.Commands;

public record RemoveCustomerCommand(string? CustomerId) : CustomerCommand
{
}
