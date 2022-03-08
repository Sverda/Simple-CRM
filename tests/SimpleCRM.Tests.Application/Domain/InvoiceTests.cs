using NUnit.Framework;
using SimpleCRM.Application.Services;
using SimpleCRM.Domain.Aggregates.InvoiceAggregate;
using SimpleCRM.Domain.Factories;
using SimpleCRM.Infrastructure.Documents.Repositories;
using SimpleCRM.Tests.Application.Helpers;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRM.Tests.Domain
{
    [TestFixture]
    public class InvoiceTests
    {
        [Test]
        public async Task PrepareDocument_ShouldReplaceFieldWithCustomerData()
        {
            // Arrange
            var templateBytes = ResourcesHelper.GetTemplateBytes();
            const string templatePath = @"c:\wordtemplate.docx";
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>
                {
                    { templatePath, new MockFileData(templateBytes) }
                }
            );
            var documentsProcessing = new DocumentsProcessingService();
            var files = new DocumentsAccessRepository(fileSystem);
            var invoice = InvoiceAggregateFactory.CreateFresh(
                1,
                templatePath,
                "Cute client",
                new Address("st. Nice", "Nicer", "00-000"));
            var templateOriginal = files.LoadAsReadOnly(templatePath);
            var templateCopy = await files.GetCopy(templateOriginal);

            // Act
            var invoiceDocument = invoice.PrepareDocument(documentsProcessing, templateOriginal, templateCopy);
            var fieldKeys = documentsProcessing.FindWithRegex(
                new MemoryStream(invoiceDocument.Content),
                ReplaceableField.KeyValueRegex).ToArray();

            // Assert
            Assert.That(invoiceDocument, Is.Not.Null);
            Assert.That(fieldKeys, Has.Length.EqualTo(2));
        }
    }
}
