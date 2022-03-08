using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Application.Repositories;
using SimpleCRM.Infrastructure.Database;
using SimpleCRM.Infrastructure.Database.Repositories;
using SimpleCRM.Infrastructure.Documents;
using SimpleCRM.Infrastructure.Documents.Repositories;

namespace SimpleCRM.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            _ = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));

            services.AddOptions<DatabaseConfig>()
                .Bind(configurationRoot.GetSection(nameof(DatabaseConfig)));
            services.AddOptions<TemplatesConfig>()
                .Bind(configurationRoot.GetSection(nameof(TemplatesConfig)));

            services.AddDbContext<CrmContext>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IDocumentsAccessRepository, DocumentsAccessRepository>();
        }
    }
}
