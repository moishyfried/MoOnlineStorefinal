using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Infrastructure.Data.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, a =>
             {
                 a.WithOwner();
                 a.Property(p => p.FirstName).IsRequired().HasMaxLength(180);
                 a.Property(p => p.LastName).IsRequired().HasMaxLength(180);
                 a.Property(p => p.Street).IsRequired().HasMaxLength(180);
                 a.Property(p => p.State).IsRequired().HasMaxLength(100);
                 a.Property(p => p.City).IsRequired().HasMaxLength(100);
                 a.Property(p => p.Zipcode).IsRequired().HasMaxLength(18);
             });
            builder.Property(i => i.Subtotal)
                 .HasColumnType("decimal(18,2)");
            builder.Property(p => p.Status).HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus),o)
                );
         
            builder.HasMany(h => h.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
