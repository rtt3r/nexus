using FluentValidation;
using Nexus.Core.Application.Persons.Commands;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Persons.Validators;

internal class RemovePersonCommandValidator : AbstractValidator<RemovePersonCommand>
{
    public RemovePersonCommandValidator()
    {
        RuleFor(c => c.PersonId)
            .NotEmpty()
                .WithMessage(Notifications.Person.ID_REQUIRED.Message)
                .WithErrorCode(Notifications.Person.ID_REQUIRED.Code);
    }
}
