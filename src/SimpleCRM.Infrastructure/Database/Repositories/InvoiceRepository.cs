using Microsoft.EntityFrameworkCore;
using SimpleCRM.Application.Repositories;
using SimpleCRM.Domain.Aggregates.InvoiceAggregate;
using SimpleCRM.Infrastructure.Database.Entities;
using SimpleCRM.Infrastructure.Database.Exceptions;
using SimpleCRM.Infrastructure.Database.Mapping;

namespace SimpleCRM.Infrastructure.Database.Repositories
{
    internal class InvoiceRepository : IInvoiceRepository
    {
        private readonly CrmContext context;

        public InvoiceRepository(CrmContext context)
        {
            this.context = context;
            //context.Database.EnsureCreated();
        }

        public async Task<Invoice> GetById(string invoiceNumber, CancellationToken cancellationToken = default)
        {
            InvoiceEntity? invoiceEntity = await context.Invoices.FindAsync(
                new object[] { invoiceNumber },
                cancellationToken: cancellationToken);
            _ = invoiceEntity ?? throw new EntityNotFoundException(typeof(InvoiceEntity), invoiceNumber);
            return invoiceEntity.MapToDomain();
        }

        public IEnumerable<Invoice> GetAll()
        {
            return context.Invoices
                .Include(i => i.Template)
                .Include(i => i.Customer)
                .Select(i => i.MapToDomain());
        }

        public async Task Add(Invoice invoice, CancellationToken cancellationToken = default)
        {
            var entity = invoice.MapToDatabase();
            await context.Invoices.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateById(Invoice invoice, CancellationToken cancellationToken = default)
        {
            var entity = invoice.MapToDatabase();
            context.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteById(string invoiceNumber, CancellationToken cancellationToken = default)
        {
            context.Invoices.Remove(new InvoiceEntity { Number = invoiceNumber });
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
