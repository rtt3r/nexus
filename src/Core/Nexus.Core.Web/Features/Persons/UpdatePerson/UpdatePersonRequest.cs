using Nexus.Core.Application.Persons.UpdatePerson;

namespace Nexus.Core.Web.Features.Persons.UpdatePerson;

public class UpdatePersonRequest(
    string FisrtName,
    string LastName,
    string Cpf,
    string? Gender,
    DateOnly? Birthdate,
    IList<UpdatePersonEmailCommand> Emails,
    IList<UpdatePersonPhoneNumberCommand> PhoneNumbers,
    IList<UpdatePersonAddressCommand> Addresses)
{
    public UpdatePersonCommand ToCommand(string id)
    {
        return new UpdatePersonCommand(
            id,
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
