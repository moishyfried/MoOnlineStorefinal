using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Infrastructure.Data.Configuration
{
    public class orderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io => { io.WithOwner(); });

            builder.Property(i => i.Price)  
                      .HasColumnType("decimal(18,2)");
        }
    }
}
