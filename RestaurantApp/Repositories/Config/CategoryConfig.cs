using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category() { CategoryId = 1, CategoryName = "Yemek"},
                new Category() { CategoryId = 2, CategoryName = "İçecek"}
            );
        }
    }
}