using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData.Map
{
    public class EmbroiderOrder_EmbroiderMap : IEntityTypeConfiguration<EmbroiderOrder_Embroider>
    {
        public void Configure(EntityTypeBuilder<EmbroiderOrder_Embroider> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.OrderId, t.EmbroiderId });
            entityBuilder.HasOne(t => t.Embroider).WithMany(t => t.EmbroiderOrder_Embroideries).HasForeignKey(b => b.EmbroiderId);
            entityBuilder.HasOne(t => t.EmbroiderOrder).WithOne(t => t.EmbroiderOrder_Embroider).HasForeignKey<EmbroiderOrder_Embroider>(b => b.OrderId);
        }
    }
}
