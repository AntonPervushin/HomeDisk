using HomeDisk.Api.Common.Access;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Commands.User.ChangePassword
{
    public sealed class ChangeUserPasswordCommandHandler
        : IRequestHandler<ChangeUserPasswordCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public ChangeUserPasswordCommandHandler(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public async Task<Unit> Handle(
            ChangeUserPasswordCommand command, 
            CancellationToken cancellationToken)
        {
            var user = AppIdentityAccessor.Current;

            await _userService.ChangePasswordAsync(
                user.UserName, 
                command.CurrentPassword, 
                command.NewPassword);

            await _authService.LogoutAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
