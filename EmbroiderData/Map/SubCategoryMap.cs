using EmbroiderData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroideryData.Map
{
    public class SubCategoryMap : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.HasIndex(e => e.SubCategoryCode).IsUnique(true);
            entityBuilder.Property(t => t.SubCategoryCode).IsRequired(true).HasMaxLength(30);
            entityBuilder.Property(t => t.CategoryId);
            entityBuilder.Property(t => t.Name).IsRequired(true).HasMaxLength(200);
            entityBuilder.HasOne(t => t.Category).WithMany(t => t.SubCategories).HasForeignKey(t => t.CategoryId);
            entityBuilder.HasMany(t => t.EmbroiderInvoiceDetail_SubCategories).WithOne(t => t.SubCategory).HasForeignKey(b => b.SubCategoryId);
            entityBuilder.HasMany(t => t.EmbroiderOrderDetail_SubCategories).WithOne(t => t.SubCategory).HasForeignKey(b => b.SubCategoryId);
        }
    }
}
