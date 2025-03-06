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

namespace Nexus.Core.Application.BusinessGroups.UpdateBusinessGroup;

internal class UpdateBusinessGroupCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, publishEndpoint, typeAdapter),
    ICommandHandler<UpdateBusinessGroupCommand, OneOf<None, AppError>>
{
    private readonly AppState appState = appState;

    public async Task<OneOf<None, AppError>> Handle(UpdateBusinessGroupCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<UpdateBusinessGroupValidator, UpdateBusinessGroupCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        BusinessGroup? buisnessGroup = await uow.BusinessGroups.GetAsync(command.Id, cancellationToken);

        if (buisnessGroup is null)
        {
            return new ResourceNotFoundError(BUSINESS_GROUP_NOT_FOUND);
        }

        buisnessGroup.SetName(command.Name);

        if (command.Description is not null)
        {
            buisnessGroup.SetDescription(command.Description);
        }

        if (command.TaxId is not null)
        {
            buisnessGroup.SetTaxId(command.TaxId);
        }

        uow.BusinessGroups.Update(buisnessGroup);

        await uow.CommitAsync(cancellationToken);

        await RaiseEvent(
            new BusinessGroupUpdatedEvent(buisnessGroup.Id, appState.User!.UserId),
            cancellationToken);

        return default(None);
    }
}
