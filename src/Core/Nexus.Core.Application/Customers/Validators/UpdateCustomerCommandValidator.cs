using FluentValidation;
using Nexus.Core.Application.Customers.Commands;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Customers.Validators;

internal class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        ValidateId();
        ValidateName();
        ValidateBirthdate();
    }

    private void ValidateId()
    {
        RuleFor(c => c.CustomerId)
            .NotEmpty()
                .WithMessage(Notifications.Customer.CUSTOMER_ID_REQUIRED.Message)
                .WithErrorCode(nameof(Notifications.Customer.CUSTOMER_ID_REQUIRED.Code));
    }

    private void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage(Notifications.Customer.CUSTOMER_NAME_REQUIRED.Message)
                .WithErrorCode(nameof(Notifications.Customer.CUSTOMER_NAME_REQUIRED.Code))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                        .Length(2, 150)
                            .WithMessage(Notifications.Customer.CUSTOMER_NAME_LENGTH_INVALID.Message)
                            .WithErrorCode(nameof(Notifications.Customer.CUSTOMER_NAME_LENGTH_INVALID.Code));
                });
    }

    private void ValidateBirthdate()
    {
        RuleFor(c => c.Birthdate)
            .NotEmpty()
                .WithMessage(Notifications.Customer.CUSTOMER_BIRTHDATE_REQUIRED.Message)
                .WithErrorCode(nameof(Notifications.Customer.CUSTOMER_BIRTHDATE_REQUIRED.Code))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Birthdate)
                        .Must(HaveMinimumAge)
                            .WithMessage(Notifications.Customer.CUSTOMER_BIRTHDATE_LENGTH_INVALID.Message)
                            .WithErrorCode(nameof(Notifications.Customer.CUSTOMER_BIRTHDATE_LENGTH_INVALID.Code));
                });

    }

    private static bool HaveMinimumAge(DateOnly? Birthdate)
        => Birthdate!.Value <= DateOnly.FromDateTime(DateTime.Today).AddYears(-18);
}
