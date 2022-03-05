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

        public async Task LoadFields(
            IDocumentsService documentsService,
            CancellationToken cancellationToken = default)
        {
            Stream templateOriginal = documentsService.LoadFileAsReadableOnly(Path);
            Stream templateCopy = await documentsService.GetDocCopy(templateOriginal, cancellationToken);
            var keys = documentsService.FindWithRegex(templateCopy, ReplaceableField.Regex);
            Fields = keys.Select(k => new ReplaceableField(k));
        }
    }
}
