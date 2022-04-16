using MediatR;

namespace HomeDisk.Api.Commands.Authentication
{
    public sealed class LoginCommand : IRequest
    {
        public LoginCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }
    }
}
