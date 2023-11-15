using Nexus.Core.Application.Events.Users;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Application.Extensions;
using Goal.Seedwork.Infra.Crosscutting.Adapters;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using MassTransit;
using UserModel = Nexus.Core.Model.Users.User;

namespace Nexus.Core.Application.Commands.Users;

public class UserCommandHandler : CommandHandlerBase,
    ICommandHandler<RegisterUserCommand, ICommandResult<UserModel>>
{
    public UserCommandHandler(
        ICoreUnitOfWork uow,
        IPublishEndpoint publishEndpoint,
        IDefaultNotificationHandler notificationHandler,
        ITypeAdapter typeAdapter,
        AppState appState)
        : base(uow, publishEndpoint, notificationHandler, typeAdapter, appState)
    {
    }

    public async Task<ICommandResult<UserModel>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        User user = await uow.Users.LoadAsync(command.Id);

        if (user != null)
        {
            return CommandResult.Success(typeAdapter.ProjectAs<UserModel>(user));
        }

        user = new User(command.Id, command.Name, command.Email);

        await uow.Users.AddAsync(user, cancellationToken);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new UserRegisteredEvent(
                    user.Id,
                    user.Name,
                    user.Email),
                cancellationToken);

            return CommandResult.Success(
                typeAdapter.ProjectAs<UserModel>(user));
        }

        return CommandResult.Failure<UserModel>(default, notificationHandler.GetNotifications());
    }
}
