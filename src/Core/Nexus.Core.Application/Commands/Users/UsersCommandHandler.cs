using Nexus.Core.Application.Events.Users;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Adapters;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using MassTransit;
using UserAccountModel = Nexus.Core.Model.Users.UserAccount;

namespace Nexus.Core.Application.Commands.Users;

public class UsersCommandHandler : CommandHandlerBase,
    ICommandHandler<CreateUserAccountCommand, ICommandResult<UserAccountModel>>
{
    public UsersCommandHandler(
        ICoreUnitOfWork uow,
        IPublishEndpoint publishEndpoint,
        IDefaultNotificationHandler notificationHandler,
        ITypeAdapter typeAdapter,
        AppState appState)
        : base(uow, publishEndpoint, notificationHandler, typeAdapter, appState)
    { }

    public async Task<ICommandResult<UserAccountModel>> Handle(CreateUserAccountCommand command, CancellationToken cancellationToken)
    {
        var userAccount = await uow.UserAccounts.LoadAsync(command.Id, cancellationToken);

        if (userAccount != null)
        {
            return CommandResult.Success(ProjectAs<UserAccountModel>(userAccount));
        }

        userAccount = UserAccountFactory.CreateNewAccount(
            command.Id,
            command.Name,
            command.Email,
            command.Username);

        await uow.UserAccounts.AddAsync(userAccount, cancellationToken);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new UserAccountCreatedEvent(userAccount.Id, userAccount.Email, userAccount.Profile.Name, userAccount.Username),
                cancellationToken);

            return CommandResult.Success(
                ProjectAs<UserAccountModel>(userAccount));
        }

        return CommandResult.Failure<UserAccountModel>(default, notificationHandler.GetNotifications());
    }
}