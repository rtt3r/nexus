using Goal.Application.Commands;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Persons.RemovePerson;

public record RemovePersonCommand(string PersonId)
    : ICommand<OneOf<None, AppError>>
{
}