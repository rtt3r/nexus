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
using BusinessGroupDto = Nexus.Core.Model.BusinessGroups.BusinessGroup;

namespace Nexus.Core.Application.BusinessGroups.CreateBusinessGroup;

internal class CreateBusinessGroupCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, publishEndpoint, typeAdapter),
    ICommandHandler<CreateBusinessGroupCommand, OneOf<BusinessGroupDto, AppError>>
{
    private readonly AppState appState = appState;

    public async Task<OneOf<BusinessGroupDto, AppError>> Handle(CreateBusinessGroupCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<CreateBusinessGroupValidator, CreateBusinessGroupCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        BusinessGroup? buisnessGroup = await uow.BusinessGroups.GetByName(
            command.Name,
            cancellationToken);

        if (buisnessGroup is not null)
        {
            return new BusinessRuleError(BUSINESS_GROUP_NAME_DUPLICATED);
        }

        buisnessGroup = new BusinessGroup(command.Name);

        if (command.Description is not null)
        {
            buisnessGroup.SetDescription(command.Description);
        }

        if (command.TaxId is not null)
        {
            buisnessGroup.SetTaxId(command.TaxId);
        }

        await uow.BusinessGroups.AddAsync(buisnessGroup, cancellationToken);

        await uow.CommitAsync(cancellationToken);

        await RaiseEvent(
            new BusinessGroupCreatedEvent(buisnessGroup.Id, appState.User!.UserId),
            cancellationToken);

        return ProjectAs<BusinessGroupDto>(buisnessGroup);
    }
}
