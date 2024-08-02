// Decompiled with JetBrains decompiler
using EmbroiderData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroideryData.Map
{
    public class EmbroiderInvoiceDetailMap : IEntityTypeConfiguration<EmbroiderInvoiceDetail>
    {
        public void Configure(
      EntityTypeBuilder<EmbroiderInvoiceDetail> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.InvoiceId);
            entityBuilder.Property(t => t.Quantity).IsRequired(true);
            entityBuilder.Property(t => t.Description).IsRequired(true);
            entityBuilder.HasOne(t => t.EmbroiderInvoiceDetail_SubCategory).WithOne(t => t.EmbroiderInvoiceDetail).HasForeignKey<EmbroiderInvoiceDetail_SubCategory>(b => b.InvoiceDetailId);
            entityBuilder.HasOne(t => t.Invoice).WithMany(t => t.InvoiceDetails).HasForeignKey(t => t.InvoiceId);
        }
    }
}
