using HomeDisk.Api.Common.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeDisk.Api.Common.Access
{
    public interface IUserService
    {
        public Task<string> CreateAsync(string userName, IEnumerable<AppIdentityRole> roles);
    }
}
