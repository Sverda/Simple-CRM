using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceTemplate : Entity<Guid>
    {
        public string Path { get; set; }

        public InvoiceTemplate(Guid id, string path) : base(id)
        {
            Path = path;
        }
    }
}
