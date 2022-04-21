using System;
using System.Security.Cryptography;

namespace HomeDisk.Api.Common.FileProcessing
{
    public static class FileHasher
    {
        public static string CalculateHash(byte[] source)
        {
            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(source));
            }
        }
    }
}
