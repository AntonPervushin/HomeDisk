using HomeDisk.Api.Common.FileProcessing;
using HomeDisk.Test.Fuxtires;
using NUnit.Framework;
using System;

namespace HomeDisk.Test.FileTests
{
    public class FileManagerTests
    {
        [Test]
        public void NotExistFile_Saved()
        {
            // Assert
            var fileName = "File";
            var fileContent = Fixture.ProvideRandomContent();
            var fixture = new Fixture();
            var before = fixture.FileStorage.GetFile(fileName);

            // Act
            fixture.FileManager.Save("", fileName, fileContent);
            var after = fixture.FileStorage.GetFile(fileName);

            // Assert
            Assert.IsNull(before);
            Assert.AreSame(after, fileContent);
        }

        [Test]
        public void ExistFileWithSameContent_NotSaved()
        {
            // Assert
            var fileName = "File";
            var fileContent = Fixture.ProvideRandomContent();
            var fixture = new Fixture();
            fixture.FileManager.Save("", fileName, fileContent);

            var before = fixture.FileStorage.GetFile(fileName);

            // Act
            fixture.FileManager.Save("", fileName, fileContent);
            var after = fixture.FileStorage.GetFile(fileName);

            // Assert
            Assert.AreSame(before, fileContent);
            Assert.AreSame(after, fileContent);
        }

        [Test]
        public void ExistFileWithAnotherContent_SavedWithAnotherName()
        {
            // Assert
            var fileName = "File";
            var fileContent = Fixture.ProvideRandomContent();
            var fixture = new Fixture();
            fixture.FileManager.Save("", fileName, fileContent);

            var before = fixture.FileStorage.GetFile(fileName);

            // Act
            var fileContent2 = Fixture.ProvideRandomContent();
            fixture.FileManager.Save("", fileName, fileContent2);
            var desiredNewFileName = fileName + " (1)";
            var after = fixture.FileStorage.GetFile(desiredNewFileName);

            // Assert
            Assert.AreSame(before, fileContent);
            Assert.AreSame(after, fileContent2);
            Assert.AreNotEqual(before, after);
        }

        [Test]
        public void ExceedMaxNameDuplicatesCount_ThowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var fileName = "File";
                var _fixture = new Fixture();
                for (var i = 0; i <= FileManager.MaxNameDuplicatesCount; i++)
                {
                    var fileContext = Fixture.ProvideRandomContent();
                    _fixture.FileManager.Save("", fileName, fileContext);
                }
            });
        }
    }
}
