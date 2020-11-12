using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Infrastructure.Data.Configuration
{
    public class DeliveyMethodsComfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.DeliveryTime).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.ShortName).IsRequired();
        }
    }
}
