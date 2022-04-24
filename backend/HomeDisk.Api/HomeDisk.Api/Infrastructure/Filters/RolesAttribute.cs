using HomeDisk.Api.Common.Access;
using HomeDisk.Api.Common.Identity;
using HomeDisk.Api.Infrastructure.ErrorHandling;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace HomeDisk.Api.Infrastructure.Filters
{
    public sealed class RolesAttribute : ActionFilterAttribute
    {
        private readonly AppIdentityRole[] _roles;

        public RolesAttribute(AppIdentityRole role)
        {
            _roles = new[] { role };
        }

        public RolesAttribute(AppIdentityRole[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var hasRole = false;
            foreach(var role in _roles)
            {
                if (AppIdentityAccessor.Current.Roles.Contains(role))
                {
                    hasRole = true;
                    break;
                }
            }
            
            if(!hasRole)
            {
                var roles = string.Join(',', _roles.Select(x => x.ToString()));

                throw new AuthorizationException($"The current user has no any of roles '{roles}'");
            }

            base.OnActionExecuting(context);
        }
    }
}
