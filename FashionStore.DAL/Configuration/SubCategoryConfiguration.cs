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
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasOne(sc => sc.ParentCategory)
        .WithMany(pc => pc.SubCategories)
        .HasForeignKey(sc => sc.ParentCategoryId);
        }
    }
}
