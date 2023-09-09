using Microsoft.EntityFrameworkCore;
using RowLevelSecurity.API.Models;

namespace RowLevelSecurity.API.Data
{
    public class ProductContext : DbContext
    {
        public Guid TenantId { get; set; }
        public string ConnectionString { get; set; }


        public ProductContext(string connectionString, Guid tenantId)
        {
            TenantId = tenantId;
            ConnectionString = connectionString;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName).HasMaxLength(50);

            var tenantId1 = Guid.NewGuid();
            var tenantId2 = Guid.NewGuid();
            var tenantId3 = Guid.NewGuid();

            modelBuilder.Entity<Product>().HasData(
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "Product 1",
                  TenantId = tenantId1
              },
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "Product 2",
                  TenantId = tenantId2
              },
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "Product 3",
                  TenantId = tenantId3
              },
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "Product 4",
                  TenantId = tenantId1
              },
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "Product 5",
                  TenantId = tenantId2
              },
              new Product
              {
                  Id = Guid.NewGuid(),
                  ProductName = "Product 6",
                  TenantId = tenantId3
              }
              );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString)
                    .AddInterceptors(new TenancySessionContextInterceptor(TenantId));
            }
            base.OnConfiguring(optionsBuilder);
        }

    }
}
