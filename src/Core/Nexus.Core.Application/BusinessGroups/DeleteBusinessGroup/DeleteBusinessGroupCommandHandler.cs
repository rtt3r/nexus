using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Domain.BusinessGroups.Aggregates;
using Nexus.Core.Domain.BusinessGroups.Events;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Crosscutting.Extensions;
using OneOf;
using OneOf.Types;
using static Nexus.Infra.Crosscutting.Constants.Notifications.BusinessGroup;
using static Nexus.Infra.Crosscutting.Constants.Notifications.Shared;

namespace Nexus.Core.Application.BusinessGroups.DeleteBusinessGroup;

internal class DeleteBusinessGroupCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, publishEndpoint, typeAdapter),
    ICommandHandler<DeleteBusinessGroupCommand, OneOf<None, AppError>>
{
    private readonly AppState appState = appState;

    public async Task<OneOf<None, AppError>> Handle(DeleteBusinessGroupCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<DeleteBusinessGroupValidator, DeleteBusinessGroupCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        BusinessGroup? buisnessGroup = await uow.BusinessGroups.GetAsync(command.Id, cancellationToken);

        if (buisnessGroup is null)
        {
            return new ResourceNotFoundError(BUSINESS_GROUP_NOT_FOUND);
        }

        buisnessGroup.Inactivate();

        if (!await CommitAsync(cancellationToken))
        {
            return new BusinessRuleError(SAVING_DATA_FAILURE);
        }

        await RaiseEvent(
            new BusinessGroupDeletedEvent(buisnessGroup.Id, appState.User!.UserId),
            cancellationToken);

        return default(None);
    }
}
