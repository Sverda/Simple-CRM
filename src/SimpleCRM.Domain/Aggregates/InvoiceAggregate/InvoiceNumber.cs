using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceNumber : ValueObject
    {
        public int Order { get; }
        public int Year { get; }

        public InvoiceNumber(int order, int year)
        {
            Order = order;
            Year = year;
        }

        public InvoiceNumber(string number)
        {
            var parts = number.Split('-');
            Order = int.Parse(parts[0]);
            Year = int.Parse(parts[1]);
        }

        public override string ToString()
        {
            return $"{Order}-{Year}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Order;
            yield return Year;
        }
    }
}
