using System.Collections.Generic;

namespace HomeDisk.Api.Controllers.Dtos.User
{
    public sealed class CreateUserRequestDto
    {
        public string UserName { get; set; }
        public IReadOnlyCollection<RoleDto> Roles { get; set; }
    }
}
