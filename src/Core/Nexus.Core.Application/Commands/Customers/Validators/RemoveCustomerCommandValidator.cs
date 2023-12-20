using FluentValidation;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Commands.Customers.Validators;

public class RemoveCustomerCommandValidator : AbstractValidator<RemoveCustomerCommand>
{
    public RemoveCustomerCommandValidator()
    {
        ValidateId();
    }

    private void ValidateId()
    {
        RuleFor(c => c.CustomerId)
            .NotEmpty()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_ID_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_ID_REQUIRED));
    }
}
