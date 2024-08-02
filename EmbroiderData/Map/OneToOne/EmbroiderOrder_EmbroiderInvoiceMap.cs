using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroiderData.Map
{
    public class EmbroiderOrder_EmbroiderInvoiceMap : IEntityTypeConfiguration<EmbroiderOrder_EmbroiderInvoice>
    {
        public void Configure(EntityTypeBuilder<EmbroiderOrder_EmbroiderInvoice> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.OrderId, t.InvoiceId });
            entityBuilder.HasOne(t => t.EmbroiderOrder).WithOne(t => t.EmbroiderOrder_EmbroiderInvoice).HasForeignKey<EmbroiderOrder_EmbroiderInvoice>(b => b.OrderId);
            entityBuilder.HasOne(t => t.EmbroiderInvoice).WithOne(t => t.EmbroiderOrder_EmbroiderInvoice).HasForeignKey<EmbroiderOrder_EmbroiderInvoice>(b => b.InvoiceId);
        }
    }
}
