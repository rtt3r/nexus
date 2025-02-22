using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Application.Persons.Commands;
using Nexus.Core.Application.Persons.Validators;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Core.Domain.Persons.Events;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Crosscutting.Extensions;
using OneOf;
using OneOf.Types;
using PersonModel = Nexus.Core.Model.Persons.NaturalPerson;

namespace Nexus.Core.Application.Persons.Handlers;

internal class PersonCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, typeAdapter),
    ICommandHandler<RegisterNaturalPersonCommand, OneOf<PersonModel, AppError>>,
    ICommandHandler<UpdatePersonCommand, OneOf<None, AppError>>,
    ICommandHandler<RemovePersonCommand, OneOf<None, AppError>>
{
    private readonly IPublishEndpoint publishEndpoint = publishEndpoint;
    private readonly AppState appState = appState;

    public async Task<OneOf<PersonModel, AppError>> Handle(RegisterNaturalPersonCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<RegisterPersonCommandValidator, RegisterNaturalPersonCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        NaturalPerson? person = await uow.NaturalPersons.GetByCpf(
            command.Cpf,
            cancellationToken);

        if (person is not null)
        {
            return new BusinessRuleError(Notifications.Person.PERSON_CPF_DUPLICATED);
        }

        DocumentType cpf = await uow.DocumentTypes.GetByName(Domains.DocumentTypes.CPF, cancellationToken);

        if (cpf is null)
        {
            return new BusinessRuleError(Notifications.Person.PERSON_CPF_INVALID);
        }

        person = new NaturalPerson(
            command.FisrtName,
            command.LastName);

        if (command.Birthdate is not null)
        {
            person.SetBirthdate(command.Birthdate);
        }

        if (command.Gender is not null)
        {
            person.SetGender(command.Gender);
        }

        person.AddDocument(cpf, command.Cpf);

        Address address;

        foreach (RegisterNaturalPersonCommand.Address item in command.Addresses)
        {
            address = person.AddAddress(
                item.Type,
                item.ZipCode,
                item.Street,
                item.Number,
                item.Neighborhood,
                item.City,
                item.State,
                item.Country);

            if (!string.IsNullOrWhiteSpace(item.Complement))
            {
                address.SetComplement(item.Complement);
            }
        }

        foreach (RegisterNaturalPersonCommand.PhoneNumber item in command.PhoneNumbers)
        {
            person.AddPhone(
                item.CountryCode,
                item.Number);
        }

        foreach (RegisterNaturalPersonCommand.Email item in command.Emails)
        {
            person.AddEmail(item.Address);
        }

        await uow.Persons.AddAsync(person, cancellationToken);

        await uow.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(
            new NaturalPersonCreatedEvent(person.Id, appState.User!.UserId),
            cancellationToken);

        return ProjectAs<PersonModel>(person);
    }

    public Task<OneOf<None, AppError>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<None, AppError>> Handle(RemovePersonCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();

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
