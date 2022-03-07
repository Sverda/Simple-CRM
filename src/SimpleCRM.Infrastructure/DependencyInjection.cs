using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Application.Repositories;
using SimpleCRM.Infrastructure.Database;
using SimpleCRM.Infrastructure.Database.Repositories;

namespace SimpleCRM.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            _ = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));

            services.AddOptions<DatabaseConfig>()
                .Bind(configurationRoot.GetSection(nameof(DatabaseConfig)));

            services.AddDbContext<CrmContext>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
        }
    }
}
