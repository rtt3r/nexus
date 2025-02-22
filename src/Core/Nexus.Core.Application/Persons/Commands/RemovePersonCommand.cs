using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Persons.Commands;

public record RemovePersonCommand(string PersonId)
    : PersonCommand<OneOf<None, AppError>>
{
}