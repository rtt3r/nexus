using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using static Nexus.Core.Application.Persons.Commands.RegisterNaturalPersonCommand;

namespace Nexus.Core.Application.Persons.Commands;

public record RegisterNaturalPersonCommand(
    string FisrtName,
    string LastName,
    string Cpf,
    string? Gender,
    DateOnly? Birthdate,
    IList<Email> Emails,
    IList<PhoneNumber> PhoneNumbers,
    IList<Address> Addresses)
    : PersonCommand<OneOf<Model.Persons.NaturalPerson, AppError>>
{
    public record Email(
        string Address)
    {
    }

    public record PhoneNumber(
        string CountryCode,
        string Number)
    {
    }

    public record Address(
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
}
