using Goal.Seedwork.Application.Commands;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Commands.Users
{
    public class RegisterUserCommand : UserCommand<ICommandResult<User>>
    {
        public RegisterUserCommand(string id, string email, string name)
        {
            Id = id;
            Email = email;
            Name = name;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}