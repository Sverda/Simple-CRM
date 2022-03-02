using SimpleCRM.Domain.Common;
using SimpleCRM.Domain.Services.Interfaces;

namespace SimpleCRM.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceTemplate : Entity<Guid>
    {
        public string Path { get; set; }

        public IEnumerable<ReplaceableField> Fields { get; set; }

        public InvoiceTemplate(Guid id, string path, IEnumerable<ReplaceableField> fields) : base(id)
        {
            Path = path;
            Fields = fields;
        }

        public async Task<Stream> GetCopy(IDocumentsService documentsService, CancellationToken cancellationToken = default)
        {
            using Stream templateDocument = documentsService.LoadTemplateFile(Path);
            Stream copy = new MemoryStream();
            await templateDocument.CopyToAsync(copy, cancellationToken);
            return copy;
        }

        public async Task LoadFields(IDocumentsService documentsService, CancellationToken cancellationToken = default)
        {
            var keys = documentsService.GetReplacableFieldKeys(ReplaceableField.KeyIndicator);
            Fields = keys.Select(k => new ReplaceableField(k));
        }
    }
}
