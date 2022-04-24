using System;
using System.IO;

namespace HomeDisk.Api.Common.FileProcessing
{
    public class FileManager
    {
        private readonly IFileStorage _fileStorage;
        private readonly string _rootPath;

        public const int MaxNameDuplicatesCount = 10;

        public FileManager(IFileStorage fileStorage, string rootPath)
        {
            _fileStorage = fileStorage;
            _rootPath = rootPath;
        }

        public void Save(string userName, string fileName, byte[] fileContent)
        {
            var fullPath = Path.Combine(_rootPath, userName, fileName);
            SaveInternal(fullPath, fullPath, fileContent, 0);
        }

        private void SaveInternal(string originalfullPath, string fullPath, byte[] fileContent, int index)
        {
            if (index >= MaxNameDuplicatesCount)
            {
                throw new InvalidOperationException("MaxNameDuplicatesCount exceeded");
            }

            if (!_fileStorage.Exists(fullPath))
            {
                _fileStorage.Save(fullPath, fileContent);
                return;
            }

            index++;
            var nextFileName = GetNextFileName(originalfullPath, index);
            if(!IsHashSame(fileContent, fullPath))
            {
                SaveInternal(originalfullPath, nextFileName, fileContent, index);
            }
        }

        private string GetNextFileName(string fullPath, int index)
        {
            string dir = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileNameWithoutExtension(fullPath);
            string fileExt = Path.GetExtension(fullPath);

            return Path.Combine(dir, fileName + " (" + index + ")" + fileExt);
        }

        private bool IsHashSame(byte[] fileToSave, string existFileFullPath)
        {
            var fileSaved = _fileStorage.GetFile(existFileFullPath);

            var fileToSaveHash = FileHasher.CalculateHash(fileToSave);
            var fileSavedHash = FileHasher.CalculateHash(fileSaved);

            return fileToSaveHash == fileSavedHash;
        }
    }
}
