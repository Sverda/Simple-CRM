using SimpleCRM.Domain.Aggregates.InvoiceAggregate;
using SimpleCRM.Domain.Services.Interfaces;

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

        public static async Task<InvoiceDocument> CreateTemp(
            IDocumentsService documentsService,
            byte[] content,
            CancellationToken cancellationToken = default)
        {
            var tempFilepath = documentsService.GetTempFilePath();
            await documentsService.SaveDoc(
                tempFilepath,
                new MemoryStream(content),
                cancellationToken);
            return new InvoiceDocument(tempFilepath, content);
        }
    }
}
