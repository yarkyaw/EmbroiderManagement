using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData.Map
{
    public class EmbroiderOrder_ProductWeightMap : IEntityTypeConfiguration<EmbroiderOrder_ProductWeight>
    {
        public void Configure(EntityTypeBuilder<EmbroiderOrder_ProductWeight> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.OrderId, t.ProductWeightId });
            entityBuilder.HasOne(t => t.ProductWeight).WithMany(t => t.EmbroiderOrder_ProductWeights).HasForeignKey(b => b.ProductWeightId);
            entityBuilder.HasOne(t => t.EmbroiderOrder).WithOne(t => t.EmbroiderOrder_ProductWeight).HasForeignKey<EmbroiderOrder_ProductWeight>(b => b.OrderId);
        }
    }
}
