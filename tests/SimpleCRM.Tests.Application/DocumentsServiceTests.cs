using NUnit.Framework;
using SimpleCRM.Application.Services;
using SimpleCRM.Domain.Aggregates.InvoiceAggregate;
using SimpleCRM.Tests.Application.Helpers;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task GetReplacableFieldKeys_ShouldReturnAllFieldKeys()
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
            Stream templateOriginal = service.LoadFileAsReadableOnly(path);
            Stream templateCopy = await service.GetDocCopy(templateOriginal);
            var fieldKeys = service.FindWithRegex(templateCopy, ReplaceableField.Regex).ToArray();

            // Assert
            Assert.That(fieldKeys, Is.Not.Empty);
            Assert.That(fieldKeys, Has.Length.EqualTo(3));
        }

        [Test]
        public async Task GetReplacableFieldKeys_ShouldKeysBeSurroundedByKeyIndicators()
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
            Stream templateOriginal = service.LoadFileAsReadableOnly(path);
            Stream templateCopy = await service.GetDocCopy(templateOriginal);
            var fieldKeys = service.FindWithRegex(templateCopy, ReplaceableField.Regex).ToArray();

            // Assert
            Assert.That(fieldKeys, Is.Not.Empty);
            foreach (var key in fieldKeys)
            {
                Assert.That(key, Does.StartWith(ReplaceableField.KeyIndicator));
                Assert.That(key, Does.EndWith(ReplaceableField.KeyIndicator));
            }
        }

        [Test]
        public async Task ReplaceParagraphsValue_ShouldReplaceKeyWithValue()
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
            Stream templateOriginal = service.LoadFileAsReadableOnly(path);
            Stream templateCopy = await service.GetDocCopy(templateOriginal);
            templateCopy = service.ReplaceParagraphsValue(templateCopy, "$ClientData$", "Rich client");

            // Assert
            var fieldKeys = service.FindWithRegex(templateCopy, ReplaceableField.Regex).ToArray();
            Assert.That(fieldKeys, Is.Not.Empty);
            Assert.That(fieldKeys, Has.Length.EqualTo(2));
        }
    }
}