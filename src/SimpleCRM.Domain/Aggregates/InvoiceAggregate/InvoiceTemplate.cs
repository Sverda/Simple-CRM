using SimpleCRM.Domain.Common;
using SimpleCRM.Domain.Services.Interfaces;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceTemplate : Entity<Guid>
    {
        public string Path { get; private set; }

        public IEnumerable<ReplaceableField>? Fields { get; private set; }

        internal InvoiceTemplate(Guid id, string path) : base(id)
        {
            Path = path;
        }

        public void LoadFields(IDocumentsService documentsService)
        {
            Stream templateOriginal = documentsService.LoadFileAsReadableOnly(Path);
            var keys = documentsService.FindWithRegex(templateOriginal, ReplaceableField.KeyValueRegex);
            Fields = keys.Select(k => new ReplaceableField(k));
        }

        public async Task<Stream> GetCopy(
            IDocumentsService documentsService,
            CancellationToken cancellationToken = default)
        {
            Stream templateOriginal = documentsService.LoadFileAsReadableOnly(Path);
            return await documentsService.GetDocCopy(templateOriginal, cancellationToken);
        }
    }
}
