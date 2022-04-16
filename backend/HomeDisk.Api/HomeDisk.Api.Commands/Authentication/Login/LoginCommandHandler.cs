using HomeDisk.Api.Common.Access;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Commands.Authentication
{
    public sealed class LoginCommandHandler
        : IRequestHandler<LoginCommand, Unit>
    {
        private readonly IAuthService _authenticationService;

        public LoginCommandHandler(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Unit> Handle(
            LoginCommand command, 
            CancellationToken cancellationToken)
        {
            await _authenticationService.LoginAsync(
                command.Login,
                command.Password,
                cancellationToken);

            return Unit.Value;
        }
    }
}
