using HomeDisk.Api.Common.Access;
using HomeDisk.Api.Common.ErrorHandling;
using HomeDisk.Api.Common.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDisk.Api.Authentication.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityOptions _identityOptions;

        public UserService(
            UserManager<AppUser> userManager, IOptions<IdentityOptions> identityOptions)
        {
            _userManager = userManager;
            _identityOptions = identityOptions.Value;
        }

        public async Task<string> CreateAsync(
            string userName, 
            IEnumerable<AppIdentityRole> roles)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user != null)
            {
                throw new BusinessException($"User '{userName}' already exists");
            }

            user = new AppUser { UserName = userName };
            var password = GeneratePassword();
            var createdResult = await _userManager.CreateAsync(user, password);
            if (!createdResult.Succeeded)
            {
                var errors = string.Join(',', createdResult.Errors.Select(e => e.Description));
                throw new BusinessException($"Can not create user '{userName}'. Errors: {errors}");
            }

            foreach(var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role.ToString());
            }

            return password;
        }

        public async Task ChangePasswordAsync(string userName, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new BusinessException($"User '{userName}' is not found");
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if(!result.Succeeded)
            {
                var errors = string.Join(',', result.Errors.Select(e => e.Description));
                throw new BusinessException($"Can not change user '{userName}' password. Errors: {errors}");
            }
        }


        private string GeneratePassword()
        {
            var opts = _identityOptions.Password;

            var allowedChars = _identityOptions.User.AllowedUserNameCharacters;
            var allowerCharsChunks = new List<string>();

            var rand = new Random(Environment.TickCount);
            var chars = new List<char>();

            if (opts.RequireUppercase)
            {
                var uppercasedChars = allowedChars.Where(ch => char.IsUpper(ch)).ToArray();
                chars.Insert(rand.Next(0, chars.Count),
                    uppercasedChars[rand.Next(0, uppercasedChars.Length)]);
                allowerCharsChunks.Add(new string(uppercasedChars));
            }

            if (opts.RequireLowercase)
            {
                var lowercasedChars = allowedChars.Where(ch => char.IsLower(ch)).ToArray();
                chars.Insert(rand.Next(0, chars.Count),
                    lowercasedChars[rand.Next(0, lowercasedChars.Length)]);
                allowerCharsChunks.Add(new string(lowercasedChars));
            }

            if (opts.RequireDigit)
            {
                var digitChars = allowedChars.Where(ch => char.IsDigit(ch)).ToArray();
                chars.Insert(rand.Next(0, chars.Count),
                    digitChars[rand.Next(0, digitChars.Length)]);
                allowerCharsChunks.Add(new string(digitChars));
            }

            if (opts.RequireNonAlphanumeric)
            {
                var nonAlphaNumericChars = allowedChars.Where(ch => !char.IsLetterOrDigit(ch)).ToArray();
                chars.Insert(rand.Next(0, chars.Count),
                    nonAlphaNumericChars[rand.Next(0, nonAlphaNumericChars.Length)]);
                allowerCharsChunks.Add(new string(nonAlphaNumericChars));
            }

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                var rcs = allowerCharsChunks[rand.Next(0, allowerCharsChunks.Count)];
                chars.Insert(rand.Next(0, chars.Count), rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
