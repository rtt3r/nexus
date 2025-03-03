using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Persons.UpdatePerson;

internal class UpdatePersonCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, publishEndpoint, typeAdapter),
    ICommandHandler<UpdatePersonCommand, OneOf<None, AppError>>
{
    private readonly AppState appState = appState;

    public Task<OneOf<None, AppError>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();

    //public async Task<OneOf<None, AppError>> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
    //{
    //    OneOf<None, InputValidationError> validation = await ValidateCommandAsync<UpdatePersonCommandValidator, UpdatePersonCommand>(command, cancellationToken);

    //    if (validation.IsError())
    //    {
    //        return validation.GetError();
    //    }

    //    Person? person = await uow.Persons.GetFromUserAsync(appState.User!.UserId, command.PersonId, cancellationToken);

    //    if (person is null)
    //    {
    //        return new ResourceNotFoundError(Notifications.Persons.NOT_FOUND);
    //    }

    //    FinancialInstitution? financialInstitution = await uow.FinancialInstitutions.GetAsync(
    //        command.FinancialInstitutionId,
    //        cancellationToken);

    //    if (financialInstitution is null)
    //    {
    //        return new BusinessRuleError(Notifications.Persons.FINANCIAL_INSTITUTION_NOT_FOUND);
    //    }

    //    person.SetName(command.Name);
    //    person.SetType(command.Type);
    //    person.SetFinancialInstitution(financialInstitution!);
    //    person.SetInitialBalance(command.InitialBalance);
    //    person.SetInitialOverdraft(command.Overdraft);

    //    if (!string.IsNullOrWhiteSpace(command.Description))
    //    {
    //        person.SetDescription(command.Description);
    //    }

    //    uow.Persons.Update(person);

    //    await uow.CommitAsync(cancellationToken);

    //    await publishEndpoint.Publish(
    //        new PersonUpdatedEvent(person.Id, appState.User!.UserId),
    //        cancellationToken);

    //    return default(None);
    //}
}
