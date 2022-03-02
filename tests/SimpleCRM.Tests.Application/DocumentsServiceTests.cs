using NUnit.Framework;
using SimpleCRM.Application.Services;
using SimpleCRM.Tests.Application.Helpers;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;

namespace SimpleCRM.Tests.Application
{
    [TestFixture]
    public class DocumentsServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldLoadTemplateFromResources()
        {
            // Arrange
            byte[] templateBytes = ResourcesHelper.GetTemplateBytes();

            // Act
            var isEmpty = templateBytes.Length == 0;

            // Assert
            Assert.IsFalse(isEmpty);
        }

        [Test]
        public void ShouldReturnAllFieldKeys()
        {
            // Arrange
            var templateBytes = ResourcesHelper.GetTemplateBytes();
            const string path = @"c:\wordtemplate.docx";
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>
                {
                    { path, new MockFileData(templateBytes) }
                }
            );
            var service = new DocumentsService(fileSystem);

            // Act
            var fieldKeys = service.GetReplacableFieldKeys(path).ToArray();

            // Assert
            Assert.That(fieldKeys, Is.Not.Empty);
            Assert.That(fieldKeys, Has.Length.EqualTo(3));
        }
    }
}