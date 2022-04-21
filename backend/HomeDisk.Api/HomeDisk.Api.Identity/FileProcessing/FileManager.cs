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
            if(!_fileStorage.Exists(fullPath))
            {
                _fileStorage.Save(fullPath, fileContent);
                return;
            }
            else if(!IsHashSame(fileContent, fullPath))
            {
                var uniqueFileName = GetUniqueFileName(fullPath);
                _fileStorage.Save(uniqueFileName, fileContent);
            }
        }

        private string GetUniqueFileName(string existFilePath)
        {
            string dir = Path.GetDirectoryName(existFilePath);
            string fileName = Path.GetFileNameWithoutExtension(existFilePath);
            string fileExt = Path.GetExtension(existFilePath);

            for (int i = 1; ; ++i)
            {
                if(i > MaxNameDuplicatesCount)
                {
                    throw new InvalidOperationException("MaxNameDuplicatesCount exceeded");
                }
                if (!_fileStorage.Exists(existFilePath))
                    return existFilePath;

                existFilePath = Path.Combine(dir, fileName + " (" + i + ")" + fileExt);
            }
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
