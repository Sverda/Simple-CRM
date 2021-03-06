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
        public static Invoice CreateExisting(
            string invoiceNumber,
            Guid templateId,
            string invoiceTemplatePath,
            Guid customerId,
            string customerName,
            string customerAddressStreet, 
            string customerAddressCity,
            string customerAddressCityCode)
        {
            var number = new InvoiceNumber(invoiceNumber);
            var template = new InvoiceTemplate(templateId, invoiceTemplatePath);
            var customer = new Customer(
                customerId, customerName,
                new Address(customerAddressStreet, customerAddressCity, customerAddressCityCode));
            return new Invoice(number, template, customer);
        }
    }
}
