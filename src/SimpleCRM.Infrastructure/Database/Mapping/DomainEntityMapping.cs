using SimpleCRM.Domain.Aggregates.InvoiceAggregate;
using SimpleCRM.Domain.Factories;
using SimpleCRM.Infrastructure.Database.Entities;

namespace SimpleCRM.Infrastructure.Database.Mapping
{
    internal static class DomainEntityMapping
    {
        public static Invoice MapToDomain(this InvoiceEntity invoiceEntity)
        {
            return InvoiceAggregateFactory.CreateExisting(
                invoiceEntity.Number,
                invoiceEntity.TemplateId,
                invoiceEntity.Template.Path,
                invoiceEntity.CustomerId,
                invoiceEntity.Customer.Name,
                invoiceEntity.Customer.AddressStreet,
                invoiceEntity.Customer.AddressCity,
                invoiceEntity.Customer.AddressCityCode);
        }

        public static InvoiceEntity MapToDatabase(this Invoice invoice)
        {
            return new InvoiceEntity
            {
                Number = invoice.Id.ToString(),
                TemplateId = invoice.Template.Id,
                Template = new TemplateEntity
                {
                    Id = invoice.Template.Id,
                    Path = invoice.Template.Path,
                },
                CustomerId = invoice.Customer.Id,
                Customer = new CustomerEntity
                {
                    Id = invoice.Customer.Id,
                    Name = invoice.Customer.Name,
                    AddressStreet = invoice.Customer.Address.Street,
                    AddressCity = invoice.Customer.Address.City,
                    AddressCityCode = invoice.Customer.Address.CityCode,
                }
            };
        }
    }
}
