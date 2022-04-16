using System;

namespace HomeDisk.Api.Infrastructure.ErrorHandling
{
    public sealed class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message) { }
    }
}
