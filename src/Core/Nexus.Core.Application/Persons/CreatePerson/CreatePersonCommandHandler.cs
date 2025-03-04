using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using PersonModel = Nexus.Core.Model.Persons.NaturalPerson;

namespace Nexus.Core.Application.Persons.CreatePerson;

internal class CreatePersonCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, publishEndpoint, typeAdapter),
    ICommandHandler<CreatePersonCommand, OneOf<PersonModel, AppError>>
{
    private readonly AppState appState = appState;

    public Task<OneOf<PersonModel, AppError>> Handle(CreatePersonCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();

    //public async Task<OneOf<PersonModel, AppError>> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    //{
    //    OneOf<None, InputValidationError> validation = await ValidateCommandAsync<CreatePersonCommandValidator, CreatePersonCommand>(command, cancellationToken);

    //    if (validation.IsError())
    //    {
    //        return validation.GetError();
    //    }

    //    NaturalPerson? person = await uow.NaturalPersons.GetByCpf(
    //        command.Cpf,
    //        cancellationToken);

    //    if (person is not null)
    //    {
    //        return new BusinessRuleError(Notifications.Person.PERSON_CPF_DUPLICATED);
    //    }

    //    person = new NaturalPerson(
    //        command.FisrtName,
    //        command.LastName,
    //        command.Cpf);

    //    if (command.Birthdate is not null)
    //    {
    //        person.SetBirthdate(command.Birthdate);
    //    }

    //    if (command.Gender is not null)
    //    {
    //        person.SetGender(command.Gender);
    //    }

    //    Address address;

    //    foreach (CreatePersonAddressCommand item in command.Addresses)
    //    {
    //        address = person.AddAddress(
    //            item.Type,
    //            item.ZipCode,
    //            item.Street,
    //            item.Number,
    //            item.Neighborhood,
    //            item.City,
    //            item.State,
    //            item.Country);

    //        if (!string.IsNullOrWhiteSpace(item.Complement))
    //        {
    //            address.SetComplement(item.Complement);
    //        }
    //    }

    //    foreach (CreatePersonPhoneNumberCommand item in command.PhoneNumbers)
    //    {
    //        person.AddContact(
    //            item.CountryCode,
    //            item.Number);
    //    }

    //    foreach (CreatePersonEmailCommand item in command.Emails)
    //    {
    //        person.AddEmail(item.Address);
    //    }

    //    await uow.Persons.AddAsync(person, cancellationToken);

    //    await uow.CommitAsync(cancellationToken);

    //    await RaiseEvent(
    //        new NaturalPersonCreatedEvent(person.Id, appState.User!.UserId),
    //        cancellationToken);

    //    return ProjectAs<PersonModel>(person);
    //}
}
