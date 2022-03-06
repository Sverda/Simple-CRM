using SimpleCRM.Domain.Common;
using SimpleCRM.Domain.Services.Interfaces;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceDocument : ValueObject
    {
        public byte[] Content { get; }
        public string? Path { get; private set; }

        public InvoiceDocument(byte[] content)
        {
            Content = content;
        }

        public InvoiceDocument(Stream stream)
        {
            var ms = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(ms);
            Content = ms.ToArray();
        }

        public async Task SaveToTemp(
            IDocumentsService documentsService,
            CancellationToken cancellationToken = default)
        {
            Path = documentsService.GetTempFilePath();
            await documentsService.SaveDoc(Path, AsStream(), cancellationToken);
        }

        public Stream AsStream()
        {
            return new MemoryStream(Content);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Content;
        }
    }
}