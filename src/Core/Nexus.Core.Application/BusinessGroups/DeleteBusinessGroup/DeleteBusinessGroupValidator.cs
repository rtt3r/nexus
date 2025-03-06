using FluentValidation;
using Nexus.Infra.Crosscutting.Validations.Fluent;
using static Nexus.Infra.Crosscutting.Constants.Notifications.BusinessGroup;

namespace Nexus.Core.Application.BusinessGroups.DeleteBusinessGroup;

internal sealed class DeleteBusinessGroupValidator : AbstractValidator<DeleteBusinessGroupCommand>
{
    public DeleteBusinessGroupValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithNotification(BUSINESS_GROUP_ID_REQUIRED);
    }
}
