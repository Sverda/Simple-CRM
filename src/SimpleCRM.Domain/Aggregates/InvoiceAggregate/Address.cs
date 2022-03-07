using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string CityCode { get; }

        public Address(string street, string city, string cityCode)
        {
            Street = street;
            City = city;
            CityCode = cityCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return CityCode;
        }

        public override string ToString() => $"{Street}, {City}, {CityCode}";
    }
}
