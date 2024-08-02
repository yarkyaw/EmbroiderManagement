using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroiderData.Map
{
    public class EmbroiderOrderDetail_SubCategoryMap : IEntityTypeConfiguration<EmbroiderOrderDetail_SubCategory>
    {
        public void Configure(EntityTypeBuilder<EmbroiderOrderDetail_SubCategory> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.OrderDetailId, t.SubCategoryId });
            entityBuilder.HasOne(t => t.EmbroiderOrderDetail).WithOne(t => t.EmbroiderOrderDetail_SubCategory).HasForeignKey<EmbroiderOrderDetail_SubCategory>(b => b.OrderDetailId);
            entityBuilder.HasOne(t => t.SubCategory).WithMany(t => t.EmbroiderOrderDetail_SubCategories).HasForeignKey(b => b.SubCategoryId);
        }
    }
}
