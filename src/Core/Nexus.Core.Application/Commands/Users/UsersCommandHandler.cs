using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using MediatR;
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
    ITypeAdapter typeAdapter,
    IGenerateUserAvatarDomainService generateUserProfileAvatarDomainService,
    AppState appState) :
    CommandHandlerBase(uow, publishEndpoint, typeAdapter, appState),
    ICommandHandler<CreateUserAccountCommand, UserAccountModel>
{
    private readonly IGenerateUserAvatarDomainService generateUserProfileAvatarDomainService = generateUserProfileAvatarDomainService;

    public async Task<UserAccountModel> Handle(CreateUserAccountCommand command, CancellationToken cancellationToken)
    {
        User? user = await uow.Users.LoadAsync(command.Id!, cancellationToken);

        if (user is not null)
        {
            return ProjectAs<UserAccountModel>(user);
        }

        user = new User(
            command.Id!,
            command.Email!,
            command.Username!);

        generateUserProfileAvatarDomainService.GenerateTemporaryAvatar(user);

        await uow.Users.AddAsync(user, cancellationToken);

        await SaveChangesAsync(cancellationToken);

        await publishEndpoint.Publish(
            new UserRegisteredEvent(user, appState.User.UserId!),
            cancellationToken);

        return ProjectAs<UserAccountModel>(user);
    }
}