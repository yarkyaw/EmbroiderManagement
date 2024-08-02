using EmbroideryData.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroiderData.Map
{
    public class EmbroiderServiceItemHistoryMap : IEntityTypeConfiguration<EmbroiderServiceItemHistory>
    {
        public void Configure(EntityTypeBuilder<EmbroiderServiceItemHistory> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.CategoryId).IsRequired();
            entityBuilder.Property(t => t.InsertedDate).IsRequired().HasColumnType(AppDataTypeConstant.DateDataType); ;
            entityBuilder.Property(t => t.Rate).IsRequired().HasColumnType(AppDataTypeConstant.DecimalDataType); ;
            entityBuilder.Property(t => t.ProductWeightId).IsRequired();
        }
    }
}
