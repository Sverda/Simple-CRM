using SimpleCRM.Domain.Common;
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

        public async Task<InvoiceDocument> PrepareDocument(
            IDocumentsService documentsService,
            CancellationToken cancellationToken = default)
        {
            Template.LoadFields(documentsService);
            if (!Template.Fields.Any(f => f.Equals(Customer.AsField)))
            {
                throw new Exception($"Template doesn't contain field {Customer.AsField}");
            }

            var templateCopy = await Template.GetCopy(documentsService, cancellationToken);
            templateCopy = documentsService.ReplaceParagraphsValue(
                templateCopy,
                Customer.AsField.ToString(),
                Customer.ToString());
            return new InvoiceDocument(templateCopy);
        }
    }
}
