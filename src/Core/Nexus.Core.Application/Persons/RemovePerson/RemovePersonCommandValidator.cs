using FluentValidation;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Persons.RemovePerson;

internal class RemovePersonCommandValidator : AbstractValidator<RemovePersonCommand>
{
    public RemovePersonCommandValidator()
    {
        RuleFor(c => c.PersonId)
            .NotEmpty()
                .WithMessage(Notifications.Person.PERSON_ID_REQUIRED.Message)
                .WithErrorCode(Notifications.Person.PERSON_ID_REQUIRED.Code);
    }
}
