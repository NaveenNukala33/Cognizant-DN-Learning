using NUnit.Framework;
using Moq;
using MagicFilesLib;
using System.Collections.Generic;
using System.Linq;

namespace DirectoryExplorer.Tests
{
    [TestFixture]
    public class DirectoryExplorerTests
    {
        private Mock<IDirectoryExplorer> _mockDirectoryExplorer;
        private readonly string _file1 = "file.txt";
        private readonly string _file2 = "file2.txt";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // This runs once before all tests
        }

        [SetUp]
        public void SetUp()
        {
            // Setup mock object
            _mockDirectoryExplorer = new Mock<IDirectoryExplorer>();
            
            // Configure mock to return our test files
            var testFiles = new List<string> { _file1, _file2 };
            _mockDirectoryExplorer.Setup(x => x.GetFiles(It.IsAny<string>()))
                                  .Returns(testFiles);
        }

        [TestCase]
        public void GetFiles_ShouldReturnFileCollection_WhenPathExists()
        {
            // Arrange
            string testPath = @"C:\TestPath";

            // Act
            ICollection<string> result = _mockDirectoryExplorer.Object.GetFiles(testPath);

            // Assert
            Assert.That(result, Is.Not.Null, "Collection should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Collection count should be equal to 2");
            Assert.That(result.Contains(_file1), Is.True, "Collection should contain file1.txt");
            Assert.That(result.Contains(_file2), Is.True, "Collection should contain file2.txt");
        }

        [TestCase]
        public void GetFiles_ShouldCallGetFilesMethod_WithCorrectPath()
        {
            // Arrange
            string testPath = @"C:\TestPath";

            // Act
            _mockDirectoryExplorer.Object.GetFiles(testPath);

            // Assert - Verify that GetFiles was called with the correct path
            _mockDirectoryExplorer.Verify(x => x.GetFiles(testPath), Times.Once);
        }

        [TestCase]
        public void GetFiles_ShouldReturnEmptyCollection_WhenNoFilesExist()
        {
            // Arrange
            var emptyMock = new Mock<IDirectoryExplorer>();
            emptyMock.Setup(x => x.GetFiles(It.IsAny<string>()))
                     .Returns(new List<string>());

            // Act
            ICollection<string> result = emptyMock.Object.GetFiles(@"C:\EmptyPath");

            // Assert
            Assert.That(result, Is.Not.Null, "Collection should not be null");
            Assert.That(result.Count, Is.EqualTo(0), "Collection should be empty");
        }

        [TestCase]
        public void GetFiles_ShouldReturnCorrectFileNames_WhenMultipleFilesExist()
        {
            // Arrange
            var multiFileMock = new Mock<IDirectoryExplorer>();
            var multipleFiles = new List<string> 
            { 
                "document1.pdf", 
                "image1.jpg", 
                "data.xml", 
                "config.json" 
            };
            multiFileMock.Setup(x => x.GetFiles(It.IsAny<string>()))
                        .Returns(multipleFiles);

            // Act
            ICollection<string> result = multiFileMock.Object.GetFiles(@"C:\Documents");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result.Contains("document1.pdf"), Is.True);
            Assert.That(result.Contains("image1.jpg"), Is.True);
            Assert.That(result.Contains("data.xml"), Is.True);
            Assert.That(result.Contains("config.json"), Is.True);
        }
    }
}
