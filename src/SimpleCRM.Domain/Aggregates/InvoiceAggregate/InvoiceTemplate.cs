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

        public void LoadFields(IDocumentsProcessingService documentsService, Stream templateOriginal)
        {
            var keys = documentsService.FindWithRegex(templateOriginal, ReplaceableField.KeyValueRegex);
            Fields = keys.Select(k => new ReplaceableField(k));
        }
    }
}
