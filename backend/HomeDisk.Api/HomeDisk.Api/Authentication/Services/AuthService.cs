using HomeDisk.Api.Common.Access;
using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDisk.Api.Authentication.Services
{
    internal sealed class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task LoginAsync(
            string login, 
            string password, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                throw new AuthenticationException("User not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return;
            }

            throw new AuthenticationException("Wrong user name or password");
        }

        public async Task LogoutAsync(CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
        }
    }
}
