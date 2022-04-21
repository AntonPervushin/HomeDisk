using HomeDisk.Api.Common.FileProcessing;
using System.Collections.Generic;

namespace HomeDisk.Test.Fuxtires
{
    internal class TestFileStorage : IFileStorage
    {
        private Dictionary<string, byte[]> _storage = new Dictionary<string, byte[]>();

        public byte[] GetFile(string fullPath)
        {
            if (Exists(fullPath))
            {
                return _storage[fullPath];
            }

            return null;
        }

        public bool Exists(string fullPath)
        {
            return _storage.ContainsKey(fullPath);
        }

        public void Save(string fillPath, byte[] content)
        {
            _storage[fillPath] = content;
        }
    }
}
