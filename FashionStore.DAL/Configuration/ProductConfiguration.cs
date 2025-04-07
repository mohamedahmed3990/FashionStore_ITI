using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.DAL.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
              builder.HasMany(p => p.ProductVariants)
            .WithOne(pv => pv.Product)
            .HasForeignKey(pv => pv.ProductId);
        }
    }
}
