using System;
using System.Linq;
using System.Security.Claims;

namespace HomeDisk.Api.Common.Identity
{
    public sealed class AppIdentity
    {
        private const string UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private const string UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        private const string UserRoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

        private readonly ClaimsIdentity _claimsIdentity;

        public AppIdentity(ClaimsIdentity claimsIdentity)
        {
            _claimsIdentity = claimsIdentity;

            if (_claimsIdentity.IsAuthenticated)
            {
                var identityIdClaim = _claimsIdentity.Claims.Single(x => x.Type.Equals(UserIdClaimType)).Value;
                UserId = int.Parse(identityIdClaim);

                var userNameClaim = _claimsIdentity.Claims.Single(x => x.Type.Equals(UserNameClaimType)).Value;
                UserName = userNameClaim;

                var userRoleClaims = _claimsIdentity.Claims.Where(x => x.Type.Equals(UserRoleClaimType))
                    .Select(x => x.Value);

                Roles = userRoleClaims.Select(x => (AppIdentityRole)Enum.Parse(typeof(AppIdentityRole), x)).ToArray();
            }
        }

        public bool IsAuthenticated => _claimsIdentity.IsAuthenticated;
        public int UserId { get; }
        public string UserName { get; }
        public AppIdentityRole[] Roles { get; }
    }
}
