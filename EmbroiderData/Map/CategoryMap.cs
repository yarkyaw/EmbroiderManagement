using EmbroiderData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbroideryData.Map
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.HasIndex(e => e.CategoryCode).IsUnique(true);
            entityBuilder.Property(t => t.CategoryCode).IsRequired(true).HasMaxLength(30);
            entityBuilder.Property(t => t.GroupId);
            entityBuilder.Property(t => t.Name).IsRequired(true).HasMaxLength(200);
            entityBuilder.HasOne(t => t.Group).WithMany(t => t.Categories).HasForeignKey(t => t.GroupId);
            entityBuilder.HasMany(t => t.EmbroiderOrder_Categories).WithOne(t => t.Category).HasForeignKey(b => b.CategoryId);
            entityBuilder.HasMany(t => t.EmbroiderInvoice_Categories).WithOne(t => t.Category).HasForeignKey(b => b.CategoryId);
            entityBuilder.HasMany(t => t.SubCategories).WithOne(t => t.Category).HasForeignKey(t => t.CategoryId);
        }
    }
}
