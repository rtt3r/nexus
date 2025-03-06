using Goal.Application.Commands;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.BusinessGroups.DeleteBusinessGroup;

public class DeleteBusinessGroupCommand : ICommand<OneOf<None, AppError>>
{
    public string Id { get; set; } = default!;
}
