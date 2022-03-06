using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class ReplaceableField : ValueObject
    {
        public static readonly char KeyIndicator = '$';
        public static readonly string KeyValueRegex = @$"\{KeyIndicator}(.+)\{KeyIndicator}";

        public string KeyValue { get; }

        public string FullExpression => $"{KeyIndicator}{KeyValue}{KeyIndicator}";

        public ReplaceableField(string keyValue)
        {
            KeyValue = keyValue.Trim(KeyIndicator);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return KeyValue;
        }

        public override string ToString() => FullExpression;
    }
}