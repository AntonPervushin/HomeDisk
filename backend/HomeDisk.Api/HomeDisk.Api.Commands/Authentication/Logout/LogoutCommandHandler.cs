using HomeDisk.Api.Common.Access;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Commands.Authentication.Logout
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IAuthService _authenticationService;

        public LogoutCommandHandler(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Unit> Handle(
            LogoutCommand command, 
            CancellationToken cancellationToken)
        {
            await _authenticationService.LogoutAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
