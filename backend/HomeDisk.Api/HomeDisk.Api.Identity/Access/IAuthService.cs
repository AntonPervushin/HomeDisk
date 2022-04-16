using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Common.Access
{
    public interface IAuthService
    {
        Task LoginAsync(string login, string password, CancellationToken cancellationToken);
        Task LogoutAsync(CancellationToken cancellationToken);
    }
}
