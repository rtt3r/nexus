using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Notifications;
using MassTransit;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Domain.Users.Events;
using Nexus.Core.Domain.Users.Services;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using UserAccountModel = Nexus.Core.Model.Users.User;

namespace Nexus.Core.Application.Commands.Users;

public class UsersCommandHandler(
    ICoreUnitOfWork uow,
    IPublishEndpoint publishEndpoint,
    IDefaultNotificationHandler notificationHandler,
    ITypeAdapter typeAdapter,
    IGenerateUserAvatarDomainService generateUserProfileAvatarDomainService,
    AppState appState) :
    CommandHandlerBase(uow, publishEndpoint, notificationHandler, typeAdapter, appState),
    ICommandHandler<CreateUserAccountCommand, ICommandResult<UserAccountModel>>
{
    private readonly IGenerateUserAvatarDomainService generateUserProfileAvatarDomainService = generateUserProfileAvatarDomainService;

    public async Task<ICommandResult<UserAccountModel>> Handle(CreateUserAccountCommand command, CancellationToken cancellationToken)
    {
        User? user = await uow.Users.LoadAsync(command.Id!, cancellationToken);

        if (user is not null)
        {
            return CommandResult.Success(ProjectAs<UserAccountModel>(user));
        }

        user = new User(
            command.Id!,
            command.Email!,
            command.Username!);

        generateUserProfileAvatarDomainService.GenerateTemporaryAvatar(user);

        await uow.Users.AddAsync(user, cancellationToken);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new UserRegisteredEvent(user, appState.User.UserId!),
                cancellationToken);

            return CommandResult.Success(ProjectAs<UserAccountModel>(user));
        }

        return CommandResult.Failure<UserAccountModel>(default, notificationHandler.GetNotifications());
    }
}