using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Notifications;
using MassTransit;
using Nexus.Core.Application.Commands.Users.Validators;
using Nexus.Core.Application.Events.Users;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Domain.Users.Services;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Constants;
using UserAccountModel = Nexus.Core.Model.Users.UserAccount;

namespace Nexus.Core.Application.Commands.Users;

public class UsersCommandHandler(
    ICoreUnitOfWork uow,
    IPublishEndpoint publishEndpoint,
    IDefaultNotificationHandler notificationHandler,
    ITypeAdapter typeAdapter,
    AppState appState,
    IGenerateUserProfileAvatarDomainService generateUserProfileAvatarDomainService) : CommandHandlerBase(uow, publishEndpoint, notificationHandler, typeAdapter, appState),
    ICommandHandler<CreateUserAccountCommand, ICommandResult<UserAccountModel>>,
    ICommandHandler<UpdateUserProfileCommand, ICommandResult>
{
    private readonly IGenerateUserProfileAvatarDomainService generateUserProfileAvatarDomainService = generateUserProfileAvatarDomainService;

    public async Task<ICommandResult<UserAccountModel>> Handle(CreateUserAccountCommand command, CancellationToken cancellationToken)
    {
        UserAccount? userAccount = await uow.UserAccounts.LoadAsync(command.Id!, cancellationToken);

        if (userAccount is not null)
        {
            return CommandResult.Success(ProjectAs<UserAccountModel>(userAccount));
        }

        userAccount = new UserAccount(
            command.Id!,
            command.Email!,
            command.Name!,
            command.Username!);

        generateUserProfileAvatarDomainService.GenerateTemporaryAvatar(userAccount);

        await uow.UserAccounts.AddAsync(userAccount, cancellationToken);

        if (await SaveChangesAsync(cancellationToken))
        {
            UserAccountModel result = ProjectAs<UserAccountModel>(userAccount);

            await publishEndpoint.Publish(
                new UserAccountCreatedEvent(result),
                cancellationToken);

            return CommandResult.Success(result);
        }

        return CommandResult.Failure<UserAccountModel>(default, notificationHandler.GetNotifications());
    }

    public async Task<ICommandResult> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        if (!await ValidateCommandAsync<UpdateUserProfileCommandValidator, UpdateUserProfileCommand>(command, cancellationToken))
        {
            return CommandResult.Failure(notificationHandler.GetNotifications());
        }

        UserAccount? userAccount = await uow.UserAccounts.LoadAsync(command.Id!, cancellationToken);

        if (userAccount is null)
        {
            await HandleDomainViolationAsync(
                nameof(ApplicationConstants.Messages.USER_NOT_FOUND),
                ApplicationConstants.Messages.USER_NOT_FOUND,
                cancellationToken);

            return CommandResult.Failure(notificationHandler.GetNotifications());
        }

        userAccount.Profile.UpdateBiography(command.Biography!);
        userAccount.Profile.UpdateBirthdate(command.Birthdate!.Value);
        userAccount.Profile.UpdateHeadline(command.Headline!);

        uow.UserAccounts.Update(userAccount);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new UserProfileUpdatedEvent(ProjectAs<UserAccountModel>(userAccount)),
                cancellationToken);

            return CommandResult.Success();
        }

        return CommandResult.Failure(notificationHandler.GetNotifications());
    }
}