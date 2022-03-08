using SimpleCRM.Domain.Aggregates.InvoiceAggregate;

namespace SimpleCRM.Domain.Factories
{
    public static class InvoiceDocumentFactory
    {
        public static InvoiceDocument Create(Stream stream)
        {
            var ms = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(ms);
            return new InvoiceDocument(string.Empty, ms.ToArray());
        }
    }
}
