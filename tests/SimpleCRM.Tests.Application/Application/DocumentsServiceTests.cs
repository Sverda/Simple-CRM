using NUnit.Framework;
using SimpleCRM.Application.Services;
using SimpleCRM.Domain.Aggregates.InvoiceAggregate;
using SimpleCRM.Infrastructure.Documents.Repositories;
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
            var documentsProcessing = new DocumentsProcessingService();
            var files = new DocumentsAccessRepository(fileSystem);

            // Act
            Stream templateOriginal = files.LoadAsReadOnly(path);
            Stream templateCopy = await files.GetCopy(templateOriginal);
            var fieldKeys = documentsProcessing.FindWithRegex(templateCopy, ReplaceableField.KeyValueRegex).ToArray();

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
            var documentsProcessing = new DocumentsProcessingService();
            var files = new DocumentsAccessRepository(fileSystem);

            // Act
            Stream templateOriginal = files.LoadAsReadOnly(path);
            Stream templateCopy = await files.GetCopy(templateOriginal);
            var fieldKeys = documentsProcessing.FindWithRegex(templateCopy, ReplaceableField.KeyValueRegex).ToArray();

            // Assert
            Assert.That(fieldKeys, Is.Not.Empty);
            foreach (var key in fieldKeys)
            {
                Assert.That(key, Does.StartWith(new string(ReplaceableField.KeyIndicator, 1)));
                Assert.That(key, Does.EndWith(new string(ReplaceableField.KeyIndicator, 1)));
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
            var documentsProcessing = new DocumentsProcessingService();
            var files = new DocumentsAccessRepository(fileSystem);

            // Act
            Stream templateOriginal = files.LoadAsReadOnly(path);
            Stream templateCopy = await files.GetCopy(templateOriginal);
            templateCopy = documentsProcessing.ReplaceParagraphsValue(templateCopy, "$ClientData$", "Rich client");

            // Assert
            var fieldKeys = documentsProcessing.FindWithRegex(templateCopy, ReplaceableField.KeyValueRegex).ToArray();
            Assert.That(fieldKeys, Is.Not.Empty);
            Assert.That(fieldKeys, Has.Length.EqualTo(2));
        }
    }
}