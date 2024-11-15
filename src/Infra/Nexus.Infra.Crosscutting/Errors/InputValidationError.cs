using FluentValidation.Results;
using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Errors;

public record InputValidationError(IEnumerable<ValidationFailure> Failures)
    : AppError(ErrorType.InputValidation, Failures.Select(f => new Notification(f.ErrorCode, f.ErrorMessage, f.PropertyName)));