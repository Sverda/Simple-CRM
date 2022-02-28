using SimpleCRM.Domain.Common;

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
    }
}
