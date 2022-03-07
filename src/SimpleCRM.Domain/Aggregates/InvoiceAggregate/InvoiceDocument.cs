using SimpleCRM.Domain.Common;
using SimpleCRM.Domain.Services.Interfaces;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceDocument : ValueObject
    {
        public string Path { get; }
        public byte[] Content { get; }

        public InvoiceDocument(string path, byte[] content)
        {
            Path = path;
            Content = content;
        }

        public InvoiceDocument(Stream stream)
        {
            Path = string.Empty;

            var ms = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(ms);
            Content = ms.ToArray();
        }

        public async Task<InvoiceDocument> SaveToTemp(
            IDocumentsService documentsService,
            CancellationToken cancellationToken = default)
        {
            var path = documentsService.GetTempFilePath();
            await documentsService.SaveDoc(path, AsStream(), cancellationToken);
            return new InvoiceDocument(path, Content);
        }

        public Stream AsStream()
        {
            return new MemoryStream(Content);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Path;
            yield return Content;
        }
    }
}