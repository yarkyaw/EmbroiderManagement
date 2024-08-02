using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData.Map
{
    public class EmbroiderOrder_CategoryMap : IEntityTypeConfiguration<EmbroiderOrder_Category>
    {
        public void Configure(EntityTypeBuilder<EmbroiderOrder_Category> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.OrderId, t.CategoryId });
            entityBuilder.HasOne(t => t.Category).WithMany(t => t.EmbroiderOrder_Categories).HasForeignKey(b => b.CategoryId);
            entityBuilder.HasOne(t => t.EmbroiderOrder).WithOne(t => t.EmbroiderOrder_Category).HasForeignKey<EmbroiderOrder_Category>(b => b.OrderId);
        }
    }
}
