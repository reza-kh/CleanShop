using Domain.Inventory.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Mapping
{
    public class InventoryItemMapping:IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Property(c => c.CreatorUserId)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.LastModifiedUserId)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(i => i.Product)
                .WithOne(p => p.InventoryItem)
                .HasForeignKey<InventoryItem>(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
