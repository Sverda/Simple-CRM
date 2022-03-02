using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class ReplaceableField : ValueObject
    {
        public static readonly string KeyIndicator = "$";

        public string Key { get; }

        public string FullExpression => $"{KeyIndicator}{Key}{KeyIndicator}";

        public ReplaceableField(string key)
        {
            Key = key;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
        }
    }
}