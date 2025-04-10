using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionStore.DAL.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.OwnsOne(o => o.ShippingAddress, sh => sh.WithOwner());

            builder.Property(o => o.Status)
                .HasConversion(
                    os => os.ToString(),
                    os => (OrderStatus) Enum.Parse(typeof(OrderStatus), os)
                );

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.ShippingFee).HasColumnType("decimal(18,2)");
        }
    }
}
