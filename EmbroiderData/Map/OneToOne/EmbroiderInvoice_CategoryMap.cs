using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData.Map
{
    public class EmbroiderInvoice_CategoryMap : IEntityTypeConfiguration<EmbroiderInvoice_Category>
    {
        public void Configure(EntityTypeBuilder<EmbroiderInvoice_Category> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.InvoiceId, t.CategoryId });
            entityBuilder.HasOne(t => t.Category).WithMany(t => t.EmbroiderInvoice_Categories).HasForeignKey(b => b.CategoryId);
            entityBuilder.HasOne(t => t.EmbroiderInvoice).WithOne(t => t.EmbroiderInvoice_Category).HasForeignKey<EmbroiderInvoice_Category>(b => b.InvoiceId);
        }
    }
}
