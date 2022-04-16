using HomeDisk.Api.Common.Identity;
using System.Threading;

namespace HomeDisk.Api.Common.Access
{
    public static class AppIdentityAccessor
    {
        private static readonly AsyncLocal<AppIdentity> Identity =
            new AsyncLocal<AppIdentity>();

        public static AppIdentity Current
        {
            get => Identity.Value;
            set => Identity.Value = value;
        }
    }
}
