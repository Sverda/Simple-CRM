namespace SimpleCRM.Infrastructure.Database.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressCityCode { get; set; }
        public ICollection<InvoiceEntity> Invoices { get; set; }
    }
}
