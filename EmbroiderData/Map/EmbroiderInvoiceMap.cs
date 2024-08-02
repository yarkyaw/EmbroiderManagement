using EmbroiderData;
using EmbroideryData.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EmbroideryData.Map
{
    public class EmbroiderInvoiceMap : IEntityTypeConfiguration<EmbroiderInvoice>
    {
        public void Configure(EntityTypeBuilder<EmbroiderInvoice> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.InvoiceNo);
            entityBuilder.HasIndex(t => t.InvoiceNo).IsUnique(true);
            entityBuilder.Property(t => t.InvoiceNo).HasMaxLength(100);
            entityBuilder.Property(t => t.OrderId);
            entityBuilder.Property(t => t.InvoiceDate).IsRequired(true).HasColumnType(AppDataTypeConstant.DateDataType);
            entityBuilder.Property(t => t.InvoiceStatus).IsRequired(true);
            entityBuilder.Property(t => t.Balance).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.Property(t => t.PaidToEmbroider).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.Property(t => t.Total).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.Property(t => t.DiposalGold).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType).HasDefaultValue<Decimal>((object)0M);
            entityBuilder.Property(t => t.ReceivedGold).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);

            entityBuilder.Property(t => t.ServiceFee).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.Property(t => t.ExcessOrLack).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.Property(t => t.ServiceFeePerItem).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);

            entityBuilder.Property(t => t.InvoiceStatus).HasConversion(v => (int)v, v => (Status)Enum.ToObject(typeof(Status), v));
            entityBuilder.HasOne(t => t.EmbroiderInvoice_Embroider).WithOne(t => t.EmbroiderInvoice).HasForeignKey<EmbroiderInvoice_Embroider>(t => t.InvoiceId);
            entityBuilder.HasMany(t => t.InvoiceDetails).WithOne(t => t.Invoice).HasForeignKey(t => t.InvoiceId);
            entityBuilder.HasOne(t => t.EmbroiderInvoice_Category).WithOne(t => t.EmbroiderInvoice).HasForeignKey<EmbroiderInvoice_Category>(b => b.InvoiceId);
            entityBuilder.HasOne(t => t.EmbroiderOrder_EmbroiderInvoice).WithOne(t => t.EmbroiderInvoice).HasForeignKey<EmbroiderOrder_EmbroiderInvoice>(b => b.InvoiceId);
            entityBuilder.HasOne(t => t.EmbroiderInvoice_ProductWeight).WithOne(t => t.EmbroiderInvoice).HasForeignKey<EmbroiderInvoice_ProductWeight>(b => b.InvoiceId);
        }
    }
}
