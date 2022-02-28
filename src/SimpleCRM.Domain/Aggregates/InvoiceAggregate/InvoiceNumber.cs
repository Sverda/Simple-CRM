using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceNumber : ValueObject
    {
        public int Order { get; set; }
        public int Year { get; set; }

        public InvoiceNumber(int order, int year)
        {
            Order = order;
            Year = year;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Order;
            yield return Year;
        }
    }
}
