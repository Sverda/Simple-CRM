﻿using SimpleCRM.Domain.Aggregates.InvoiceAggregate;

namespace SimpleCRM.Application.Repositories
{
    public interface IInvoiceRepository
    {
        Task Add(Invoice invoice, CancellationToken cancellationToken = default);
        Task DeleteById(string invoiceNumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<Invoice>> GetAll(CancellationToken cancellationToken = default);
        Task<Invoice> GetById(string invoiceNumber, CancellationToken cancellationToken = default);
        Task UpdateById(Invoice invoice, CancellationToken cancellationToken = default);
    }
}