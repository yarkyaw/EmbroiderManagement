using EmbroiderData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EmbroideryData.Map
{
    public class EmbroiderOrderDetailMap : IEntityTypeConfiguration<EmbroiderOrderDetail>
    {
        public void Configure(
      EntityTypeBuilder<EmbroiderOrderDetail> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.OrderId);
            entityBuilder.Property(t => t.Quantity).IsRequired(true);
            entityBuilder.Property(t => t.Description);
            entityBuilder.Property(t => t.Ratio).IsRequired(true);
            entityBuilder.Property(t => t.MaterialType).HasConversion(v => (int)v, v => (MaterialType)Enum.ToObject(typeof(MaterialType), v));
            entityBuilder.HasOne(t => t.Order).WithMany(t => t.OrderDetails).HasForeignKey(t => t.OrderId);
            entityBuilder.HasOne(t => t.EmbroiderOrderDetail_SubCategory).WithOne(t => t.EmbroiderOrderDetail).HasForeignKey<EmbroiderOrderDetail_SubCategory>(b => b.OrderDetailId);
        }
    }
}
