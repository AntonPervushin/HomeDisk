using HomeDisk.Api.Common.Access;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Commands.User
{
    public sealed class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<string> Handle(
            CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            return _userService.CreateAsync(command.UserName, command.Roles);
        }
    }
}
