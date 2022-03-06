using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class Customer : Entity<Guid>, IReplaceable
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public ReplaceableField AsField => new("ClientData");

        public Customer(Guid id, string name, Address address) : base(id)
        {
            Name = name;
            Address = address;
        }

        public override string ToString() => $"{Name}{Environment.NewLine}{Address}";
    }
}
