using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Customer(Guid id, string name, Address address) : base(id)
        {
            Name = name;
            Address = address;
        }
    }
}
