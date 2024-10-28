using FluentValidation;
using Nexus.Core.Application.Customers.Commands;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Customers.Validators;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
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
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_ID_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_ID_REQUIRED));
    }

    private void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_NAME_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_NAME_REQUIRED))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                        .Length(2, 150)
                            .WithMessage(ApplicationConstants.Messages.CUSTOMER_NAME_LENGTH_INVALID)
                            .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_NAME_LENGTH_INVALID));
                });
    }

    private void ValidateBirthdate()
    {
        RuleFor(c => c.Birthdate)
            .NotEmpty()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_REQUIRED))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Birthdate)
                        .Must(HaveMinimumAge)
                            .WithMessage(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_LENGTH_INVALID)
                            .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_LENGTH_INVALID));
                });

    }

    private static bool HaveMinimumAge(DateOnly? Birthdate)
        => Birthdate!.Value <= DateOnly.FromDateTime(DateTime.Today).AddYears(-18);
}
