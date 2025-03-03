using Goal.Application.Commands;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;

namespace Nexus.Core.Application.Persons.CreatePerson;

public record CreatePersonCommand(
    string FisrtName,
    string LastName,
    string Cpf,
    string? Gender,
    DateOnly? Birthdate,
    IList<CreatePersonEmailCommand> Emails,
    IList<CreatePersonPhoneNumberCommand> PhoneNumbers,
    IList<CreatePersonAddressCommand> Addresses)
    : ICommand<OneOf<Model.Persons.NaturalPerson, AppError>>
{
}

public record CreatePersonEmailCommand(
        string Address)
{
}

public record CreatePersonPhoneNumberCommand(
    string CountryCode,
    string Number)
{
}

public record CreatePersonAddressCommand(
    string Type,
    string ZipCode,
    string Street,
    string Number,
    string Complement,
    string Neighborhood,
    string City,
    string State,
    string Country)
{
}