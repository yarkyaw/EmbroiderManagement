using EmbroiderData;
using EmbroideryData.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroideryData.Map
{
    public class ProductWeightMap : IEntityTypeConfiguration<ProductWeight>
    {
        public void Configure(EntityTypeBuilder<ProductWeight> entityBuilder)
        {
            entityBuilder.HasKey(t => (object)t.Id);
            entityBuilder.HasIndex(t => t.Name).IsUnique(true);
            entityBuilder.Property(t => t.LocalizeName).IsRequired(true);
            entityBuilder.Property(t => t.Gram).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.HasMany(t => t.EmbroiderOrder_ProductWeights).WithOne(t => t.ProductWeight).HasForeignKey(b => b.ProductWeightId);
            entityBuilder.HasMany(t => t.EmbroiderInvoice_ProductWeights).WithOne(t => t.ProductWeight).HasForeignKey(b => b.ProductWeightId);
        }
    }
}
