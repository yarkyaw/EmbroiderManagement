using EmbroideryData;
using System;
using System.Linq.Expressions;

namespace EmbroiderData.DTO
{
    public class EmbroiderOrderDetailDTO
    {
        public int OrderId { get; set; }

        public int SubCategoryId { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int Ratio { get; set; }

        public MaterialType MaterialType { get; set; }

        public static Expression<Func<EmbroiderOrderDetail, EmbroiderOrderDetailDTO>> Projection
        {
            get
            {
                return z => new EmbroiderOrderDetailDTO()
                {
                    Description = z.Description,
                    MaterialType = z.MaterialType,
                    OrderId = z.OrderId,
                    Quantity = z.Quantity,
                    Ratio = z.Quantity,
                    SubCategoryId = z.EmbroiderOrderDetail_SubCategory.SubCategoryId
                };
            }
        }
    }
}
