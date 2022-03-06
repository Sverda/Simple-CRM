namespace SimpleCRM.Infrastructure.Database.Entities
{
    public class TemplateEntity
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public ICollection<InvoiceEntity> Invoices { get; set; }
    }
}