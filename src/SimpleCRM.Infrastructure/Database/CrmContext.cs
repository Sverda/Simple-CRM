using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleCRM.Infrastructure.Database.Entities;

namespace SimpleCRM.Infrastructure.Database
{
    internal class CrmContext : DbContext
    {
        private readonly IOptions<DatabaseConfig> options;

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<TemplateEntity> Templates { get; set; }

        public CrmContext(IOptions<DatabaseConfig> options)
        {
            this.options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(options.Value.ConnectionStrings.Default);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .IsRequired();

            modelBuilder.Entity<TemplateEntity>()
                .HasMany(t => t.Invoices)
                .WithOne(i => i.Template)
                .IsRequired();
        }
    }
}
