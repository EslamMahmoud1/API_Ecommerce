using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ModelConfig
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.productBrand).WithMany().HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.productType).WithMany().HasForeignKey(p => p.TypeId);
        }
    }
}
