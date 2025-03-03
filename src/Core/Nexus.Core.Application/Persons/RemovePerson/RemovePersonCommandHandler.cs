using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Persons.RemovePerson;

internal class RemovePersonCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, publishEndpoint, typeAdapter),
    ICommandHandler<RemovePersonCommand, OneOf<None, AppError>>
{
    private readonly AppState appState = appState;

    public Task<OneOf<None, AppError>> Handle(RemovePersonCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();

    //public async Task<OneOf<None, AppError>> Handle(RemovePersonCommand command, CancellationToken cancellationToken)
    //{
    //    OneOf<None, InputValidationError> validation = await ValidateCommandAsync<RemovePersonCommandValidator, RemovePersonCommand>(command, cancellationToken);

    //    if (validation.IsError())
    //    {
    //        return validation.GetError();
    //    }

    //    Person? person = await uow.Persons.GetFromUserAsync(appState.User!.UserId, command.PersonId, cancellationToken);

    //    if (person is null)
    //    {
    //        return new ResourceNotFoundError(Notifications.Persons.NOT_FOUND);
    //    }

    //    uow.Persons.Remove(person);

    //    await uow.CommitAsync(cancellationToken);

    //    await publishEndpoint.Publish(
    //        new PersonRemovedEvent(command.PersonId, appState.User!.UserId),
    //        cancellationToken);

    //    return default(None);
    //}
}
