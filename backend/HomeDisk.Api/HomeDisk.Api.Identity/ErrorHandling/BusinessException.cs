using System;

namespace HomeDisk.Api.Common.ErrorHandling
{
    public sealed class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}
