using SimpleCRM.Domain.Common;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceDocument : ValueObject
    {
        public string Path { get; }
        public byte[] Content { get; }

        internal InvoiceDocument(string path, byte[] content)
        {
            Path = path;
            Content = content;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Path;
            yield return Content;
        }
    }
}