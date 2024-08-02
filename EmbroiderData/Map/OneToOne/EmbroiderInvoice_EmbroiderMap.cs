using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData.Map
{
    public class EmbroiderInvoice_EmbroiderMap : IEntityTypeConfiguration<EmbroiderInvoice_Embroider>
    {
        public void Configure(EntityTypeBuilder<EmbroiderInvoice_Embroider> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.InvoiceId, t.EmbroiderId });
            entityBuilder.HasOne(t => t.Embroider).WithMany(t => t.EmbroiderInvoice_Embroideries).HasForeignKey(b => b.EmbroiderId);
            entityBuilder.HasOne(t => t.EmbroiderInvoice).WithOne(t => t.EmbroiderInvoice_Embroider).HasForeignKey<EmbroiderInvoice_Embroider>(b => b.InvoiceId);
        }
    }
}
