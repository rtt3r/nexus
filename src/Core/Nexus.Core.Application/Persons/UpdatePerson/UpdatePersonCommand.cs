using Goal.Application.Commands;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.Persons.UpdatePerson;

public record UpdatePersonCommand(
    string Id,
    string FisrtName,
    string LastName,
    string Cpf,
    string? Gender,
    DateOnly? Birthdate,
    IList<UpdatePersonEmailCommand> Emails,
    IList<UpdatePersonPhoneNumberCommand> PhoneNumbers,
    IList<UpdatePersonAddressCommand> Addresses)
    : ICommand<OneOf<None, AppError>>
{
}

public record UpdatePersonEmailCommand(string Address)
{
}

public record UpdatePersonPhoneNumberCommand(string CountryCode, string Number)
{
}

public record UpdatePersonAddressCommand(
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
