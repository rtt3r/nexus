using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Customers.Commands;

public record UpdateCustomerCommand(string? CustomerId, string? Name, string? Email, DateOnly? Birthdate)
    : CustomerCommand<OneOf<None, AppError>>
{
}
