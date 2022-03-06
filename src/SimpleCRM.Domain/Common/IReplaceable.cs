using SimpleCRM.Domain.Aggregates.InvoiceAggregate;

namespace SimpleCRM.Domain.Common
{
    internal interface IReplaceable
    {
        ReplaceableField AsField { get; }
    }
}
