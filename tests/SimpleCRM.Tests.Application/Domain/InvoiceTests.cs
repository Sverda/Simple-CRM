using NUnit.Framework;
using SimpleCRM.Application.Services;
using SimpleCRM.Domain.Aggregates.InvoiceAggregate;
using SimpleCRM.Tests.Application.Helpers;
using System;
using System.Collections.Generic;
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
            var service = new DocumentsService(fileSystem);
            var template = new InvoiceTemplate(Guid.NewGuid(), templatePath);
            var customer = new Customer(
                Guid.NewGuid(),
                "Cute client",
                new Address("st. Nice", "Nicer", "00-000"));
            var invoice = new Invoice(new InvoiceNumber(1, 2022), template, customer);

            // Act
            var invoiceDocument = await invoice.PrepareDocument(service);
            var fieldKeys = service.FindWithRegex(
                invoiceDocument.AsStream(),
                ReplaceableField.KeyValueRegex).ToArray();

            // Assert
            Assert.That(invoiceDocument, Is.Not.Null);
            Assert.That(fieldKeys, Has.Length.EqualTo(2));
        }
    }
}
