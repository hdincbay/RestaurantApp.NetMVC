using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product() { ProductId = 1, ProductName = "Kebap", Price = 120, ImageUrl="/images/1.jpg", CategoryId = 1},
                new Product() { ProductId = 2, ProductName = "Kola", Price = 15, ImageUrl="/images/2.jpg", CategoryId = 2}
            );

        }
    }
}