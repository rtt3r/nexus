using FluentValidation;
using Nexus.Finance.Application.Accounts.Commands;
using Nexus.Finance.Domain.Accounts.Aggregates;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Finance.Application.Accounts.Validators;

internal class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(c => c.AccountId)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.ID_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.ID_REQUIRED.Code);

        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.NAME_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.NAME_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                        .Length(3, 64)
                            .WithMessage(Notifications.Accounts.NAME_LENGTH_INVALID.Message)
                            .WithErrorCode(Notifications.Accounts.NAME_LENGTH_INVALID.Code);
                });

        RuleFor(c => c.Description)
            .Length(1, 256)
                .WithMessage(Notifications.Accounts.DESCRIPTION_LENGTH_INVALID.Message)
                .WithErrorCode(Notifications.Accounts.DESCRIPTION_LENGTH_INVALID.Code)
            .When(c => !string.IsNullOrWhiteSpace(c.Description));

        RuleFor(c => c.Type)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.TYPE_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.TYPE_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Type)
                        .IsEnumName(typeof(AccountType), false)
                            .WithMessage(Notifications.Accounts.TYPE_LENGTH_INVALID.Message)
                            .WithErrorCode(Notifications.Accounts.TYPE_LENGTH_INVALID.Code);
                });

        RuleFor(c => c.FinancialInstitutionId)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.FINANCIAL_INSTITUTION_ID_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.FINANCIAL_INSTITUTION_ID_REQUIRED.Code);

        RuleFor(c => c.Icon)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.ICON_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.ICON_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                        .Length(1, 16)
                            .WithMessage(Notifications.Accounts.ICON_LENGTH_INVALID.Message)
                            .WithErrorCode(Notifications.Accounts.ICON_LENGTH_INVALID.Code);
                });

        RuleFor(c => c.InitialBalance)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.INITIAL_BALANCE_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.INITIAL_BALANCE_REQUIRED.Code);

        RuleFor(c => c.Overdraft)
            .NotEmpty()
                .WithMessage(Notifications.Accounts.OVERDRAFT_REQUIRED.Message)
                .WithErrorCode(Notifications.Accounts.OVERDRAFT_REQUIRED.Code);
    }
}
