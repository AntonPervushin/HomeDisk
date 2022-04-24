using MediatR;

namespace HomeDisk.Api.Commands.User.ChangePassword
{
    public sealed class ChangeUserPasswordCommand : IRequest
    {
        public ChangeUserPasswordCommand(string currentPassword, string newPassword)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }

        public string CurrentPassword { get; }
        public string NewPassword { get; }
    }
}
