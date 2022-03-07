using SimpleCRM.Domain.Common;
using SimpleCRM.Domain.Factories;
using SimpleCRM.Domain.Services.Interfaces;

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

        public async Task<InvoiceDocument> SaveToTemp(
            IDocumentsService documentsService,
            CancellationToken cancellationToken = default)
        {
            return await InvoiceDocumentFactory.CreateTemp(documentsService, Content, cancellationToken);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Path;
            yield return Content;
        }
    }
}