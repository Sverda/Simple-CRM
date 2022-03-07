using SimpleCRM.Domain.Aggregates.InvoiceAggregate;

namespace SimpleCRM.Domain.Factories
{
    public static class InvoiceAggregateFactory
    {
        public static Invoice CreateFresh(
            int invoiceCount,
            string invoiceTemplatePath,
            string customerName,
            Address customerAddress)
        {
            var invoiceNumber = new InvoiceNumber(invoiceCount, DateTime.Now.Year);
            var template = new InvoiceTemplate(Guid.NewGuid(), invoiceTemplatePath);
            var customer = new Customer(Guid.NewGuid(), customerName, customerAddress);
            return new Invoice(invoiceNumber, template, customer);
        }
    }
}
