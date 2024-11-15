using FluentValidation;
using Nexus.Core.Application.Customers.Commands;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Customers.Validators;

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
                .WithMessage(Notifications.Customer.CUSTOMER_ID_REQUIRED.Message)
                .WithErrorCode(nameof(Notifications.Customer.CUSTOMER_ID_REQUIRED.Code));
    }
}
