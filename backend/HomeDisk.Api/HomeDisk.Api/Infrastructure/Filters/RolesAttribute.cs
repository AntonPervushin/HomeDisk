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
            foreach(var role in _roles)
            {
                if (!AppIdentityAccessor.Current.Roles.Contains(role))
                {
                    throw new AuthorizationException($"The current user has no role '{ role }'");
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}
