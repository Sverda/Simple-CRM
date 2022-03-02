using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceDocument : ValueObject
    {
        public byte[] Content { get; }

        public InvoiceDocument(byte[] content)
        {
            Content = content;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Content;
        }
    }
}