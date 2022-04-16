using HomeDisk.Api.Common.Access;
using HomeDisk.Api.Common.Identity;
using HomeDisk.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeDisk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Roles(AppIdentityRole.User)]
        public async Task CreateAsync()
        {
            var user = AppIdentityAccessor.Current;
            await Task.CompletedTask;
        }
    }
}
