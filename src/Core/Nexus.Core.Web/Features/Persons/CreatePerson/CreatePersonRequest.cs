using Nexus.Core.Application.Persons.CreatePerson;

namespace Nexus.Core.Web.Features.Persons.CreatePerson;

public class CreatePersonRequest(
    string FisrtName,
    string LastName,
    string Cpf,
    string? Gender,
    DateOnly? Birthdate,
    IList<CreatePersonEmailCommand> Emails,
    IList<CreatePersonPhoneNumberCommand> PhoneNumbers,
    IList<CreatePersonAddressCommand> Addresses)
{
    public CreatePersonCommand ToCommand()
    {
        return new CreatePersonCommand(
            FisrtName,
            LastName,
            Cpf,
            Gender,
            Birthdate,
            Emails,
            PhoneNumbers,
            Addresses);
    }
}
