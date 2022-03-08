using SimpleCRM.Domain.Common;
using SimpleCRM.Domain.Factories;
using SimpleCRM.Domain.Services.Interfaces;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class Invoice : Entity<InvoiceNumber>, IAggregateRoot
    {
        public Customer Customer { get; set; }
        public InvoiceTemplate Template { get; set; }

        internal Invoice(InvoiceNumber number, InvoiceTemplate template, Customer customer) : base(number)
        {
            Template = template;
            Customer = customer;
        }

        public InvoiceDocument PrepareDocument(
            IDocumentsProcessingService documentsService,
            Stream templateOriginal,
            Stream templateCopy)
        {
            Template.LoadFields(documentsService, templateOriginal);
            if (!Template.Fields.Any(f => f.Equals(Customer.AsField)))
            {
                throw new Exception($"Template doesn't contain field {Customer.AsField}");
            }

            templateCopy = documentsService.ReplaceParagraphsValue(
                templateCopy,
                Customer.AsField.ToString(),
                Customer.ToString());
            return InvoiceDocumentFactory.Create(templateCopy);
        }
    }
}
