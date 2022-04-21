namespace HomeDisk.Api.Common.FileProcessing
{
    /// <summary>
    /// Wrapping interface for testing support
    /// </summary>
    public interface IFileStorage
    {
        bool Exists(string fullPath);
        byte[] GetFile(string fullPath);
        void Save(string fullPath, byte[] content);
    }
}
