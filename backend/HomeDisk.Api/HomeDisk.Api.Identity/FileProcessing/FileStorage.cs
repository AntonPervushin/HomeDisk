using System.IO;

namespace HomeDisk.Api.Common.FileProcessing
{
    public class FileStorage : IFileStorage
    {
        public byte[] GetFile(string fullPath)
        {
            return File.ReadAllBytes(fullPath);
        }

        public bool Exists(string fullPath)
        {
            return File.Exists(fullPath);
        }

        public void Save(string fullPath, byte[] content)
        {
            File.WriteAllBytes(fullPath, content);
        }
    }
}
