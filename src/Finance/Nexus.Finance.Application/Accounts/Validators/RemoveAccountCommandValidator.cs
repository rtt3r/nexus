using FluentValidation;
using Nexus.Finance.Application.Accounts.Commands;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Finance.Application.Accounts.Validators;

internal class RemoveAccountCommandValidator : AbstractValidator<RemoveAccountCommand>
{
    public RemoveAccountCommandValidator()
    {
        RuleFor(c => c.AccountId)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.ID_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.ID_REQUIRED.Code);
    }
}
