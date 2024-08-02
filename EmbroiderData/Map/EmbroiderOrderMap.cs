using EmbroiderData;
using EmbroideryData.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EmbroideryData.Map
{
    public class EmbroiderOrderMap : IEntityTypeConfiguration<EmbroiderOrder>
    {
        public void Configure(EntityTypeBuilder<EmbroiderOrder> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.HasIndex(t => t.OrderNo).IsUnique(true);
            entityBuilder.Property(t => t.OrderNo).HasMaxLength(100);
            entityBuilder.Property(t => t.OrderDate).IsRequired(true).HasColumnType(AppDataTypeConstant.DateDataType);
            entityBuilder.Property(t => t.PaidGold).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.Property(t => t.PaidJewel).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.Property(t => t.OrderStatus).IsRequired(true);
            entityBuilder.Property(t => t.OrderStatus).HasConversion(v => (int)v, (v => (Status)Enum.ToObject(typeof(Status), v)));
            entityBuilder.Property(t => t.OrderType).HasConversion(v => (int)v, v => (OrderType)Enum.ToObject(typeof(OrderType), v));
            entityBuilder.HasMany(t => t.OrderDetails).WithOne(t => t.Order).HasForeignKey(t => t.OrderId);
            entityBuilder.HasOne(t => t.EmbroiderOrder_Embroider).WithOne(t => t.EmbroiderOrder).HasForeignKey<EmbroiderOrder_Embroider>(t => t.OrderId);
            entityBuilder.HasOne(t => t.EmbroiderOrder_ProductWeight).WithOne(t => t.EmbroiderOrder).HasForeignKey<EmbroiderOrder_ProductWeight>(b => b.OrderId);

            entityBuilder.HasOne(t => t.EmbroiderOrder_Category).WithOne(t => t.EmbroiderOrder).HasForeignKey<EmbroiderOrder_Category>(b => b.OrderId);
            entityBuilder.HasOne(t => t.EmbroiderOrder_EmbroiderInvoice).WithOne(t => t.EmbroiderOrder).HasForeignKey<EmbroiderOrder_EmbroiderInvoice>(b => b.OrderId);
        }
    }
}
