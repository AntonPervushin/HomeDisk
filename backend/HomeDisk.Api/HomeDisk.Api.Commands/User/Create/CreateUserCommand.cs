using HomeDisk.Api.Common.Identity;
using MediatR;
using System.Collections.Generic;

namespace HomeDisk.Api.Commands.User.Create
{
    public sealed class CreateUserCommand : IRequest<string>
    {
        public CreateUserCommand(string userName, IEnumerable<AppIdentityRole> roles)
        {
            UserName = userName;
            Roles = roles;
        }

        public string UserName { get; }
        public IEnumerable<AppIdentityRole> Roles { get; }
    }
}
