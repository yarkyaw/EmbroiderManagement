using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData.Map
{
    public class EmbroiderInvoiceDetail_SubCategoryMap : IEntityTypeConfiguration<EmbroiderInvoiceDetail_SubCategory>
    {
        public void Configure(EntityTypeBuilder<EmbroiderInvoiceDetail_SubCategory> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.InvoiceDetailId, t.SubCategoryId });
            entityBuilder.HasOne(t => t.SubCategory).WithMany(t => t.EmbroiderInvoiceDetail_SubCategories).HasForeignKey(b => b.SubCategoryId);
            entityBuilder.HasOne(t => t.EmbroiderInvoiceDetail).WithOne(t => t.EmbroiderInvoiceDetail_SubCategory).HasForeignKey<EmbroiderInvoiceDetail_SubCategory>(b => b.InvoiceDetailId);
        }
    }
}
