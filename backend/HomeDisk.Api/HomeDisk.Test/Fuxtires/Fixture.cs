using HomeDisk.Api.Common.FileProcessing;
using System;

namespace HomeDisk.Test.Fuxtires
{
    public class Fixture
    {
        public Fixture()
        {
            FileStorage = new TestFileStorage();
            FileManager = new FileManager(FileStorage, "");
        }
        public FileManager FileManager { get; }
        public IFileStorage FileStorage { get; }

        public static byte[] ProvideRandomContent()
        {
            var rnd = new Random();
            var byteArray = new byte[100];
            rnd.NextBytes(byteArray);

            return byteArray;
        }
    }
}
