using FluentValidation;
using Nexus.Core.Application.Persons.Commands;

namespace Nexus.Core.Application.Persons.Validators;

internal class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
    }
}
