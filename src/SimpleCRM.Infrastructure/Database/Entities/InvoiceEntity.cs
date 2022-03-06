namespace SimpleCRM.Infrastructure.Database.Entities
{
    public class InvoiceEntity
    {
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public Guid TemplateId { get; set; }
        public TemplateEntity Template { get; set; }
    }
}
