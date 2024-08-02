using EmbroideryData.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroideryData.Map
{
    public class EmbroiderMap : IEntityTypeConfiguration<Embroider>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<Embroider> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.HasIndex(t => t.EmbroiderCode).IsUnique(true);
            entityBuilder.Property(t => t.EmbroiderCode).HasMaxLength(100);
            entityBuilder.Property(t => t.Name).IsRequired(true);
            entityBuilder.Property(t => t.Phone).IsRequired(true);
            entityBuilder.Property(t => t.OpeningBalance).IsRequired(true).HasColumnType(AppDataTypeConstant.DecimalDataType);
            entityBuilder.HasMany(t => t.EmbroiderOrder_Embroideries).WithOne(t => t.Embroider).HasForeignKey(t => t.EmbroiderId);
            entityBuilder.HasMany(t => t.EmbroiderInvoice_Embroideries).WithOne(t => t.Embroider).HasForeignKey(t => t.EmbroiderId);
        }

        #endregion
    }
}
