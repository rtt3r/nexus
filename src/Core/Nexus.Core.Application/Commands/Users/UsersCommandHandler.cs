using Nexus.Core.Application.Events.Users;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Adapters;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using MassTransit;
using UserProfileModel = Nexus.Core.Model.Users.UserProfile;

namespace Nexus.Core.Application.Commands.Users;

public class UsersCommandHandler : CommandHandlerBase,
    ICommandHandler<CreateUserProfileCommand, ICommandResult<UserProfileModel>>
{
    public UsersCommandHandler(
        ICoreUnitOfWork uow,
        IPublishEndpoint publishEndpoint,
        IDefaultNotificationHandler notificationHandler,
        ITypeAdapter typeAdapter,
        AppState appState)
        : base(uow, publishEndpoint, notificationHandler, typeAdapter, appState)
    { }

    public async Task<ICommandResult<UserProfileModel>> Handle(CreateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await uow.UserProfiles.LoadAsync(command.Id, cancellationToken);

        if (profile != null)
        {
            return CommandResult.Success(ProjectAs<UserProfileModel>(profile));
        }

        profile = new UserProfile(command.Id);

        await uow.UserProfiles.AddAsync(profile, cancellationToken);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new UserProfileCreatedEvent(profile.Id),
                cancellationToken);

            return CommandResult.Success(
                ProjectAs<UserProfileModel>(profile));
        }

        return CommandResult.Failure<UserProfileModel>(default, notificationHandler.GetNotifications());
    }
}