using Domain.Orders.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Property(o => o.CreationDate)
                .IsRequired();

            builder.Property(c => c.CreatorUserId)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.LastModifiedUserId)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsMany(o => o.Items, item =>
            {
                item.WithOwner().HasForeignKey("OrderId");
                item.Property<Guid>("Id");
                item.HasKey("Id");

                item.Property(i => i.ProductId).IsRequired();
                item.Property(i => i.Quantity).IsRequired();
                item.Property(i => i.UnitPrice)
                    .HasPrecision(18, 2)
                    .IsRequired();
            });

        }
    }
}
