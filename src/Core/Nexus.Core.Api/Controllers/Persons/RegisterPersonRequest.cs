using Nexus.Core.Application.Persons.Commands;
using static Nexus.Core.Application.Persons.Commands.RegisterNaturalPersonCommand;

namespace Nexus.Core.Api.Controllers.Persons;

public class RegisterPersonRequest(
    string FisrtName,
    string LastName,
    string Cpf,
    string? Gender,
    DateOnly? Birthdate,
    IList<Email> Emails,
    IList<PhoneNumber> PhoneNumbers,
    IList<Address> Addresses)
{
    public RegisterNaturalPersonCommand ToCommand()
    {
        return new RegisterNaturalPersonCommand(
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
