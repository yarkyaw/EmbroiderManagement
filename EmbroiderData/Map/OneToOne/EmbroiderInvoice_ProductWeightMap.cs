using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroiderData.Map
{
    public class EmbroiderInvoice_ProductWeightMap : IEntityTypeConfiguration<EmbroiderInvoice_ProductWeight>
    {
        public void Configure(EntityTypeBuilder<EmbroiderInvoice_ProductWeight> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.InvoiceId, t.ProductWeightId });
            entityBuilder.HasOne(t => t.ProductWeight).WithMany(t => t.EmbroiderInvoice_ProductWeights).HasForeignKey(b => b.ProductWeightId);
            entityBuilder.HasOne(t => t.EmbroiderInvoice).WithOne(t => t.EmbroiderInvoice_ProductWeight).HasForeignKey<EmbroiderInvoice_ProductWeight>(b => b.InvoiceId);
        }
    }
}
