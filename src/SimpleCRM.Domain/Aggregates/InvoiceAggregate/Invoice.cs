using SimpleCRM.Domain.Common;
using SimpleCRM.Domain.Services.Interfaces;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class Invoice : Entity<InvoiceNumber>, IAggregateRoot
    {
        public Customer Customer { get; set; }
        public InvoiceTemplate Template { get; set; }

        public Invoice(InvoiceNumber number, InvoiceTemplate template, Customer customer) : base(number)
        {
            Template = template;
            Customer = customer;
        }

        public async Task<InvoiceDocument> PrepareDocument(
            IDocumentsService documentsService,
            CancellationToken cancellationToken = default)
        {
            await Template.LoadFields(documentsService, cancellationToken);
            return null;
        }
    }
}
