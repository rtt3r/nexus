using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Customers.Commands;

public record RemoveCustomerCommand(string? CustomerId) : CustomerCommand<OneOf<None, AppError>>
{
}
