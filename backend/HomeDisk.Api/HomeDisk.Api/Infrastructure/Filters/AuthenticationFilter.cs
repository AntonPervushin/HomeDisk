using HomeDisk.Api.Common.Access;
using HomeDisk.Api.Common.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace HomeDisk.Api.Infrastructure.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AppIdentityAccessor.Current = new AppIdentity(context.HttpContext.User.Identities.FirstOrDefault());

            base.OnActionExecuting(context);
        }
    }
}
