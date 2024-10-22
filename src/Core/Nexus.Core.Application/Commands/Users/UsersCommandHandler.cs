using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Domain.Users.Events;
using Nexus.Core.Domain.Users.Services;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using UserModel = Nexus.Core.Model.Users.User;

namespace Nexus.Core.Application.Commands.Users;

public class UsersCommandHandler(
    ICoreUnitOfWork uow,
    IPublishEndpoint publishEndpoint,
    ITypeAdapter typeAdapter,
    IGenerateUserAvatarDomainService generateUserProfileAvatarDomainService,
    AppState appState) :
    CommandHandlerBase(uow, publishEndpoint, typeAdapter, appState),
    ICommandHandler<CreateUserCommand, UserModel>
{
    private readonly IGenerateUserAvatarDomainService generateUserProfileAvatarDomainService = generateUserProfileAvatarDomainService;

    public async Task<UserModel> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await uow.Users.LoadAsync(command.Id!, cancellationToken);

        if (user is not null)
        {
            await publishEndpoint.Publish(
                new UserRegisteredEvent(
                    user.Id,
                    appState.User.UserId!),
                cancellationToken);

            return ProjectAs<UserModel>(user);
        }

        user = User.CreateUser(
            command.Id!,
            command.Name!,
            command.Email!,
            command.Username!);

        generateUserProfileAvatarDomainService.GenerateTemporaryAvatar(user);

        await uow.Users.AddAsync(user, cancellationToken);

        await SaveChangesAsync(cancellationToken);

        await publishEndpoint.Publish(
            new UserRegisteredEvent(
                user.Id,
                appState.User.UserId!),
            cancellationToken);

        return ProjectAs<UserModel>(user);
    }
}